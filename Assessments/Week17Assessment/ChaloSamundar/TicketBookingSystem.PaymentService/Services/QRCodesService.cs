using QRCoder;
namespace TicketBookingSystem.QRCodeService.Services
{
    public class QRCodesService: IQRCodeService
    {
        public byte[] GenerateQRCode(string data)
        {
            using (var generator = new QRCodeGenerator())
            {
                var qrData = generator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrData);

                using (var bitmap = qrCode.GetGraphic(20))
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }
    }
}
