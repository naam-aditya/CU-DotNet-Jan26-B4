using System.Text.Json;
using TicketBookingSystem.Mvc.ViewModels;

namespace TicketBookingSystem.Mvc.Services
{
    public class PaymentApiClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly string _baseUrl;

        public PaymentApiClient(IHttpClientFactory factory, IConfiguration config)
        {
            _factory = factory;
            _baseUrl = "https://localhost:7005/";
        }

        private HttpClient CreateClient() => _factory.CreateClient("payment");

        // ── CARD: calls ProcessPayment with card details ──
        public async Task<(bool Success, string? Error)> ProcessCardPayment(
            int bookingId, decimal amount, string cardNumber,
            string? cardHolderName, string? expiry, string? cvv)
        {
            try
            {
                var client = CreateClient();
                var payload = new
                {
                    bookingId,
                    amount,
                    paymentMethod = "CARD",
                    cardNumber,
                    cardHolderName,
                    expiry,
                    cvv
                };
                var res = await client.PostAsJsonAsync($"{_baseUrl}api/payments/process", payload);

                if (!res.IsSuccessStatusCode)
                {
                    var errBody = await res.Content.ReadAsStringAsync();
                    Console.WriteLine($"[PaymentApiClient] Card payment rejected ({(int)res.StatusCode}): {errBody}");
                    return (false, $"Payment failed: {errBody}");
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(string? QRCodeBase64, string? Error)> GenerateUpiQR(int bookingId, decimal amount)
        {
            try
            {
                var client = CreateClient();
                var payload = new { bookingId, amount, paymentMethod = "UPI" };
                var res = await client.PostAsJsonAsync($"{_baseUrl}api/payments/process", payload);

                if (!res.IsSuccessStatusCode)
                {
                    // Surface the actual server message so we can debug
                    var errBody = await res.Content.ReadAsStringAsync();
                    Console.WriteLine($"[PaymentApiClient] PaymentService rejected ({(int)res.StatusCode}): {errBody}");
                    return (null, $"Payment service error: {(int)res.StatusCode} {res.StatusCode} — {errBody}");
                }

                var json = await res.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<JsonElement>(json);

                // Try all possible property name casings
                string? qrCode = null;

                if (data.TryGetProperty("qRCode", out var p1)) qrCode = p1.GetString();
                else if (data.TryGetProperty("QRCode", out var p2)) qrCode = p2.GetString();
                else if (data.TryGetProperty("qrCode", out var p3)) qrCode = p3.GetString();
                else if (data.TryGetProperty("qr_code", out var p4)) qrCode = p4.GetString();

                if (string.IsNullOrEmpty(qrCode))
                    return (null, $"QR property not found in response: {json}");

                return (qrCode, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(bool Success, string? Error)> ConfirmUpiPayment(int bookingId)
        {
            try
            {
                var client = CreateClient();
                var res = await client.PostAsync($"{_baseUrl}api/payments/confirm/{bookingId}", null);
                return res.IsSuccessStatusCode ? (true, null) : (false, $"Error: {res.StatusCode}");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(string? OrderId, int Amount, string? KeyId, string? Error)> CreateRazorpayOrder(int bookingId, decimal amount)
        {
            try
            {
                var client = CreateClient();
                var payload = new { bookingId, amount, currency = "INR" };
                var res = await client.PostAsJsonAsync($"{_baseUrl}api/payments/razorpay/create-order", payload);

                if (!res.IsSuccessStatusCode)
                    return (null, 0, null, $"Payment service error: {res.StatusCode}");

                var json = await res.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<JsonElement>(json);

                // Match exact property names from Swagger response
                string? orderId = null;
                int amt = 0;
                string? keyId = null;

                if (data.TryGetProperty("orderId", out var p1)) orderId = p1.GetString();
                else if (data.TryGetProperty("OrderId", out var p2)) orderId = p2.GetString();

                if (data.TryGetProperty("amount", out var p3)) amt = p3.GetInt32();
                else if (data.TryGetProperty("Amount", out var p4)) amt = p4.GetInt32();

                if (data.TryGetProperty("keyId", out var p5)) keyId = p5.GetString();
                else if (data.TryGetProperty("KeyId", out var p6)) keyId = p6.GetString();

                if (string.IsNullOrEmpty(orderId))
                    return (null, 0, null, $"OrderId not found in response: {json}");

                return (orderId, amt, keyId, null);
            }
            catch (Exception ex)
            {
                return (null, 0, null, ex.Message);
            }
        }

        public async Task<(int PaymentId, string? Error)> VerifyPayment(VerifyPaymentViewModel model)
        {
            try
            {
                var client = CreateClient();
                var payload = new
                {
                    bookingId = model.BookingId,
                    razorpayOrderId = model.RazorpayOrderId,
                    razorpayPaymentId = model.RazorpayPaymentId,
                    razorpaySignature = model.RazorpaySignature
                };
                var res = await client.PostAsJsonAsync($"{_baseUrl}api/payments/razorpay/verify", payload);
                if (!res.IsSuccessStatusCode)
                    return (0, $"Verification failed: {res.StatusCode}");

                var json = await res.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<JsonElement>(json);
                var paymentId = data.GetProperty("paymentId").GetInt32();
                return (paymentId, null);
            }
            catch (Exception ex)
            {
                return (0, ex.Message);
            }
        }
    }
}
