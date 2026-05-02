using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.PaymentService.Data;
using TicketBookingSystem.PaymentService.DTOs;
using TicketBookingSystem.PaymentService.Models;
using TicketBookingSystem.PaymentService.Services;

namespace TicketBookingSystem.PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        // ✅ GET ALL PAYMENTS
        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _service.GetAllPayments();
            return Ok(payments);
        }

        // ✅ GET PAYMENT BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _service.GetPaymentById(id);

            if (payment == null)
                return NotFound("Payment not found");

            return Ok(payment);
        }

        // 🔥 PROCESS PAYMENT (UPI + CARD)
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment(PaymentDTO dto)
        {
            
            var result = await _service.ProcessPayment(dto);
            return Ok(result);
        }

        [HttpPost("confirm/{bookingId}")]
        public async Task<IActionResult> ConfirmUPIPayment(int bookingId)
        {
            var response = await _service.ConfirmUPIPayment(bookingId);
            return Ok(response);
        }

        // ❌ DELETE PAYMENT
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _service.DeletePayment(id);

            if (!result)
                return NotFound("Payment not found");

            return NoContent();
        }

        // STEP 1 — Create Razorpay Order
        [HttpPost("razorpay/create-order")]
        public async Task<IActionResult> CreateRazorpayOrder(CreateOrderDTO dto)
        {
            var result = await _service.CreateRazorpayOrder(dto);
            return Ok(result);
        }

        // STEP 2 — Verify payment after user pays
        [HttpPost("razorpay/verify")]
        public async Task<IActionResult> VerifyPayment(VerifyPaymentDTO dto)
        {
            var result = await _service.VerifyAndSavePayment(dto);
            return Ok(result);
        }
    }
}
