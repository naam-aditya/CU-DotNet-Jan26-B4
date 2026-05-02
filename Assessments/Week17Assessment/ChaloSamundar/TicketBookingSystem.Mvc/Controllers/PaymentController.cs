using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.Mvc.Models;
using TicketBookingSystem.Mvc.Services;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Controllers
{
    public class PaymentController : Controller
    {
        private readonly PaymentApiClient _paymentService;
        //private readonly CabinReservationApiClient _cabinReservation;
        private readonly IConfiguration _config;

        public PaymentController(
            PaymentApiClient paymentService,
            //CabinReservationApiClient cabinReservation,
            IConfiguration config)
        {
            _paymentService = paymentService;
            //_cabinReservation = cabinReservation;
            _config = config;
        }

        private void SaveSession(int bookingId, decimal amount)
        {
            HttpContext.Session.SetInt32("PayBookingId", bookingId);
            HttpContext.Session.SetString("PayAmount", amount.ToString(
                System.Globalization.CultureInfo.InvariantCulture));
        }

        private (int bookingId, decimal amount) GetSession()
        {
            var bookingId = HttpContext.Session.GetInt32("PayBookingId") ?? 0;
            var amtStr = HttpContext.Session.GetString("PayAmount") ?? "0";
            decimal.TryParse(amtStr,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var amount);
            return (bookingId, amount);
        }

        private string GetBookingReference()
            => HttpContext.Session.GetString("BookingReference") ?? string.Empty;

        [HttpGet]
        public IActionResult Index(int bookingId, decimal amount)
        {
            if (bookingId > 0 && amount > 0)
                SaveSession(bookingId, amount);
            else
                (bookingId, amount) = GetSession();
            return View(new PaymentViewModel { BookingId = bookingId, Amount = amount });
        }

        // ── CARD ──
        [HttpPost]
        public async Task<IActionResult> InitiateCard(PaymentViewModel model)
        {
            (model.BookingId, model.Amount) = GetSession();

            if (string.IsNullOrWhiteSpace(model.CardNumber) ||
                model.CardNumber.Replace(" ", "").Length != 16)
            {
                model.PaymentMethod = "CARD";
                model.ErrorMessage = "Please enter a valid 16-digit card number.";
                return View("Index", model);
            }

            var (success, error) = await _paymentService.ProcessCardPayment(
                model.BookingId, model.Amount,
                model.CardNumber.Replace(" ", ""),
                model.CardHolderName, model.Expiry, model.CVV);

            if (!success)
            {
                // ── Release cabin if payment fails ──
                //await _cabinReservation.ReleaseAsync(GetBookingReference());

                model.PaymentMethod = "CARD";
                model.ErrorMessage = error ?? "Card payment failed.";
                return View("Index", model);
            }

            // ── Confirm cabin on payment success ──
            //await ConfirmCabinAsync();

            return RedirectToAction("Success", new
            {
                paymentId = 0,
                bookingId = model.BookingId,
                amount = model.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture),
                status = "Success",
                paymentMethod = "CARD"
            });
        }

        // ── UPI QR ──
        [HttpPost]
        public async Task<IActionResult> GenerateQR(PaymentViewModel model)
        {
            (model.BookingId, model.Amount) = GetSession();
            var (qrCode, error) = await _paymentService.GenerateUpiQR(model.BookingId, model.Amount);
            model.PaymentMethod = "UPI";
            if (error != null) { model.ErrorMessage = $"Could not generate QR: {error}"; return View("Index", model); }
            model.QRCodeBase64 = qrCode;
            return View("Index", model);
        }

        // ── UPI Confirm ──
        [HttpPost]
        public async Task<IActionResult> ConfirmUpi(PaymentViewModel model)
        {
            (model.BookingId, model.Amount) = GetSession();
            var (success, error) = await _paymentService.ConfirmUpiPayment(model.BookingId);
            if (!success)
            {
                //await _cabinReservation.ReleaseAsync(GetBookingReference());
                model.PaymentMethod = "UPI";
                model.ErrorMessage = error ?? "Confirmation failed";
                return View("Index", model);
            }

            //await ConfirmCabinAsync();

            return RedirectToAction("Success", new
            {
                paymentId = 0,
                bookingId = model.BookingId,
                amount = model.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture),
                status = "Success",
                paymentMethod = "UPI"
            });
        }

        // ── UPI ID ──
        [HttpPost]
        public async Task<IActionResult> UpiIdPay(PaymentViewModel model)
        {
            (model.BookingId, model.Amount) = GetSession();
            if (string.IsNullOrEmpty(model.UpiId) || !model.UpiId.Contains('@'))
            {
                model.PaymentMethod = "UPI";
                model.ErrorMessage = "Please enter a valid UPI ID (e.g. name@upi)";
                return View("Index", model);
            }

            var (success, error) = await _paymentService.ConfirmUpiPayment(model.BookingId);
            if (!success)
            {
                //await _cabinReservation.ReleaseAsync(GetBookingReference());
                model.PaymentMethod = "UPI";
                model.ErrorMessage = error ?? "UPI payment failed";
                return View("Index", model);
            }

            //await ConfirmCabinAsync();

            return RedirectToAction("Success", new
            {
                paymentId = 0,
                bookingId = model.BookingId,
                amount = model.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture),
                status = "Success",
                paymentMethod = "UPI_ID"
            });
        }

        // ── Net Banking ──
        [HttpPost]
        public async Task<IActionResult> InitiateNB(PaymentViewModel model)
        {
            (model.BookingId, model.Amount) = GetSession();
            if (string.IsNullOrEmpty(model.SelectedBank))
            {
                model.ErrorMessage = "Please select a bank first";
                return View("Index", model);
            }
            var (orderId, rzpAmount, keyId, error) =
                await _paymentService.CreateRazorpayOrder(model.BookingId, model.Amount);
            if (error != null) { model.ErrorMessage = $"Could not initiate net banking: {error}"; return View("Index", model); }
            model.RazorpayOrderId = orderId;
            model.RazorpayAmount = rzpAmount;
            model.RazorpayKeyId = keyId ?? _config["Razorpay:KeyId"];
            model.PaymentMethod = "NET_BANKING";
            return View("Index", model);
        }

        // ── Razorpay Verify ──
        [HttpPost]
        public async Task<IActionResult> Verify(VerifyPaymentViewModel model)
        {
            var (bookingId, amount) = GetSession();
            if (model.BookingId == 0) model.BookingId = bookingId;
            if (model.Amount == 0) model.Amount = amount;

            var (paymentId, error) = await _paymentService.VerifyPayment(model);
            if (error != null)
            {
                //await _cabinReservation.ReleaseAsync(GetBookingReference());
                return RedirectToAction("Index", new { bookingId, amount });
            }

            //await ConfirmCabinAsync();

            return RedirectToAction("Success", new
            {
                paymentId,
                bookingId = model.BookingId,
                amount = model.Amount.ToString(System.Globalization.CultureInfo.InvariantCulture),
                status = "Success",
                paymentMethod = model.PaymentMethod,
                razorpayPaymentId = model.RazorpayPaymentId
            });
        }

        // ── Success ──
        [HttpGet]
        public IActionResult Success(int paymentId, int bookingId, decimal amount,
                                     string status, string paymentMethod, string? razorpayPaymentId)
        {
            HttpContext.Session.Remove("PayBookingId");
            HttpContext.Session.Remove("PayAmount");
            // Keep BookingReference for display on success page if needed

            return View(new PaymentSuccessViewModel
            {
                PaymentId = paymentId,
                BookingId = bookingId,
                Amount = amount,
                Status = status,
                PaymentMethod = paymentMethod,
                RazorpayPaymentId = razorpayPaymentId
            });
        }

        // ── Helper: confirm cabin after successful payment ──
        //private async Task ConfirmCabinAsync()
        //{
        //    var bookingRef = GetBookingReference();
        //    if (!string.IsNullOrEmpty(bookingRef))
        //    {
        //        var (confirmed, err) = await _cabinReservation.ConfirmAsync(bookingRef);
        //        Console.WriteLine(confirmed
        //            ? $"[Payment] Cabin confirmed for booking {bookingRef}"
        //            : $"[Payment] Cabin confirm failed for {bookingRef}: {err}");
        //    }
        //}
    }
}