using System.Security.Claims;

namespace TicketBookingSystem.BookingService.Helpers
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            // 1. Look for the claim in common JWT locations
            var claim = user?.FindFirst(ClaimTypes.NameIdentifier)
                ?? user?.FindFirst("sub")
                ?? user?.FindFirst("id");
            var claimValue = claim?.Value;

            // 2. Check if the claim exists and is a valid number
            if (string.IsNullOrWhiteSpace(claimValue))
            {
                throw new InvalidOperationException("User ID not found in token");
            }
            return claimValue;
        }  
    }
}
