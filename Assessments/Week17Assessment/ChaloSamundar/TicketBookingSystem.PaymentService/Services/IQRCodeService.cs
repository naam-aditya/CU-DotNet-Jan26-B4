namespace TicketBookingSystem.QRCodeService.Services
{
    public interface IQRCodeService
    {
        byte[] GenerateQRCode(string data);
    }
}
