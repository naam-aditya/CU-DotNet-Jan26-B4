namespace Vagabond.TravelDestinationService.Dtos
{
    public class CreateDestinationDto
    {
        public string? CityName { get; set; }
        public string? Country { get; set; }
        public string? Description { get; set; }
        public int Rating { get; set; }
    }
}