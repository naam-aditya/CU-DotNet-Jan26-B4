using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketBookingSystem.QRCodeService.Services;

namespace TicketBookingSystem.QRCodeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeService _service;

        public QRCodeController(IQRCodeService service)
        {
            _service = service;
        }

        [HttpPost("generate")]
        public IActionResult GenerateQR([FromBody] string data)
        {
            var qr = _service.GenerateQRCode(data);
            return File(qr, "image/png");
        }
    }
}
