using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;
using TicketBookingSystem.PaymentService.DTOs;
using TicketBookingSystem.PaymentService.Exceptions;
using TicketBookingSystem.PaymentService.Models;
using TicketBookingSystem.PaymentService.Repositories;
using Payment = TicketBookingSystem.PaymentService.Models.Payment;

namespace TicketBookingSystem.PaymentService.Services
{
    public class PaymentsService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public PaymentsService(IPaymentRepository repo, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _repo = repo;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<IEnumerable<Payment>> GetAllPayments()
            => await _repo.GetAllAsync();

        public async Task<Payment> GetPaymentById(int id)
            => await _repo.GetByIdAsync(id);

        // ── STEP 1: Frontend calls this to get a Razorpay Order ID ──────────
        public async Task<object> CreateRazorpayOrder(CreateOrderDTO dto)
        {
            var keyId = _config["Razorpay:KeyId"]!;
            var keySecret = _config["Razorpay:KeySecret"]!;

            var client = new RazorpayClient(keyId, keySecret);

            var options = new Dictionary<string, object>
            {
                { "amount", (int)(dto.Amount * 100) },   // paise
                { "currency", dto.Currency },
                { "receipt", $"booking_{dto.BookingId}_{DateTime.UtcNow.Ticks}" },
                { "payment_capture", 1 }
            };

            var order = client.Order.Create(options);
            string orderId = order["id"].ToString();

            // Save a Pending payment record
            var payment = new Payment
            {
                BookingId = dto.BookingId,
                Amount = dto.Amount,
                PaymentMethod = "RAZORPAY",
                Status = "Pending",
                PaymentDate = DateTime.UtcNow
            };
            await _repo.AddAsync(payment);

            return new
            {
                OrderId = orderId,
                Currency = dto.Currency,
                Amount = (int)(dto.Amount * 100),
                KeyId = keyId,
                PaymentId = payment.PaymentId
            };
        }

        // ── STEP 2: Frontend calls this after user completes payment ─────────
        public async Task<object> VerifyAndSavePayment(VerifyPaymentDTO dto)
        {
            var keySecret = _config["Razorpay:KeySecret"]!;

            // Signature verification
            string payload = $"{dto.RazorpayOrderId}|{dto.RazorpayPaymentId}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(keySecret));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
            var computedSignature = BitConverter.ToString(computedHash)
                                                .Replace("-", "")
                                                .ToLower();

            if (computedSignature != dto.RazorpaySignature)
                throw new PaymentFailedException("Payment signature verification failed");

            // Update DB record to Success
            var payment = await _repo.GetByBookingIdAsync(dto.BookingId)
                          ?? throw new NotFoundException("Payment record not found");

            payment.Status = "Success";
            await _repo.UpdateAsync(payment);

            return new
            {
                Message = "Payment Verified Successfully",
                PaymentId = payment.PaymentId,
                Status = payment.Status,
                RazorpayPaymentId = dto.RazorpayPaymentId
            };
        }

        // ── Legacy ProcessPayment (UPI QR + Card kept as-is) ─────────────────
        public async Task<object> ProcessPayment(PaymentDTO dto)
        {
            var payment = new Payment
            {
                BookingId = dto.BookingId,
                Amount = dto.Amount,
                PaymentMethod = dto.PaymentMethod,
                Status = "Pending",
                PaymentDate = DateTime.UtcNow
            };
            await _repo.AddAsync(payment);

            if (dto.PaymentMethod?.Equals("UPI", StringComparison.OrdinalIgnoreCase) == true)
            {
                // The standalone QRCodeService is no longer in the active solution; generate
                // the QR via a free public endpoint instead. It returns a PNG image directly.
                var client = _httpClientFactory.CreateClient();
                string upiUrl = $"upi://pay?pa=smartcruise@upi&pn=SmartCruise&am={dto.Amount}&cu=INR";

                string qrApi =
                    $"https://api.qrserver.com/v1/create-qr-code/?size=300x300&data={Uri.EscapeDataString(upiUrl)}";

                byte[] qrImage;
                try
                {
                    qrImage = await client.GetByteArrayAsync(qrApi);
                }
                catch (Exception ex)
                {
                    throw new Exception($"QR Service Failed: {ex.Message}", ex);
                }

                return new
                {
                    Type      = "QR",
                    Message   = "Scan QR to Pay",
                    PaymentId = payment.PaymentId,
                    QRCode    = Convert.ToBase64String(qrImage)
                };
            }
            else if (dto.PaymentMethod?.Equals("CARD", StringComparison.OrdinalIgnoreCase) == true)
            {
                if (!string.IsNullOrEmpty(dto.CardNumber) && dto.CardNumber.Length == 16)
                    payment.Status = "Success";
                else
                    throw new PaymentFailedException("Invalid Card Details");

                await _repo.UpdateAsync(payment);
                return new { Type = "CARD", Message = "Card Payment Successful", PaymentId = payment.PaymentId, Status = payment.Status };
            }

            throw new BadRequestException("Invalid Payment Method");
        }

        public async Task<string> ConfirmUPIPayment(int bookingId)
        {
            var payment = await _repo.GetByBookingIdAsync(bookingId)
                          ?? throw new NotFoundException("Payment not found");
            payment.Status = "Success";
            await _repo.UpdateAsync(payment);
            return "UPI Payment Successful";
        }

        public async Task<bool> DeletePayment(int id)
        {
            var payment = await _repo.GetByIdAsync(id)
                          ?? throw new NotFoundException("Payment not found");
            await _repo.DeleteAsync(payment);
            return true;
        }
    }
}
