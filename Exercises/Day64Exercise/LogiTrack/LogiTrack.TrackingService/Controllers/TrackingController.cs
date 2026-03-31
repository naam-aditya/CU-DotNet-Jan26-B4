using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogiTrack.TrackingService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackingController : ControllerBase
{
    [HttpGet("gps")]
    [Authorize(Roles = "Manager")]
    public IActionResult GetGpsData()
    {
        var locations = new[]
        {
            new {
                TruckId = "DL01AB1234",
                Address = "Connaught Place, New Delhi, DL, 110001",
                City = "New Delhi",
                State = "DL",
                ZipCode = "110001",
                Latitude = 28.6315,
                Longitude = 77.2167
            },
            new {
                TruckId = "MH02CD5678",
                Address = "Bandra West, Mumbai, MH, 400050",
                City = "Mumbai",
                State = "MH",
                ZipCode = "400050",
                Latitude = 19.0596,
                Longitude = 72.8295
            },
            new {
                TruckId = "KA03EF9012",
                Address = "MG Road, Bengaluru, KA, 560001",
                City = "Bengaluru",
                State = "KA",
                ZipCode = "560001",
                Latitude = 12.9756,
                Longitude = 77.6050
            },
            new {
                TruckId = "WB04GH3456",
                Address = "Salt Lake Sector V, Kolkata, WB, 700091",
                City = "Kolkata",
                State = "WB",
                ZipCode = "700091",
                Latitude = 22.5769,
                Longitude = 88.4320
            },
            new {
                TruckId = "TS05IJ7890",
                Address = "Hitech City, Hyderabad, TS, 500081",
                City = "Hyderabad",
                State = "TS",
                ZipCode = "500081",
                Latitude = 17.4435,
                Longitude = 78.3772
            },
            new {
                TruckId = "PK06KL1122",
                Address = "Gulberg III, Lahore, Punjab, 54660",
                City = "Lahore",
                State = "Punjab",
                ZipCode = "54660",
                Latitude = 31.5204,
                Longitude = 74.3587
            },
            new {
                TruckId = "PK07MN3344",
                Address = "Clifton Block 5, Karachi, Sindh, 75600",
                City = "Karachi",
                State = "Sindh",
                ZipCode = "75600",
                Latitude = 24.8138,
                Longitude = 67.0305
            },
            new {
                TruckId = "PK08OP5566",
                Address = "Blue Area, Islamabad, ICT, 44000",
                City = "Islamabad",
                State = "ICT",
                ZipCode = "44000",
                Latitude = 33.6844,
                Longitude = 73.0479
            },
            new {
                TruckId = "PK09QR7788",
                Address = "Saddar, Rawalpindi, Punjab, 46000",
                City = "Rawalpindi",
                State = "Punjab",
                ZipCode = "46000",
                Latitude = 33.5651,
                Longitude = 73.0169
            },
            new {
                TruckId = "PK10ST9900",
                Address = "University Road, Peshawar, KP, 25000",
                City = "Peshawar",
                State = "KP",
                ZipCode = "25000",
                Latitude = 34.0151,
                Longitude = 71.5249
            }
        };

        return Ok(locations);
    }
}
