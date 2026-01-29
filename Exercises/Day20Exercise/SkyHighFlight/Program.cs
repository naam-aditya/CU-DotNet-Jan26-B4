namespace SkyHighFlightAggregator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Flight> flights = [
                new() { FlightNumber = "12345", Price = 5490, Duration = TimeSpan.FromHours(3), DepartureTime = DateTime.UtcNow },
                new() { FlightNumber = "23456", Price = 3790, Duration = TimeSpan.FromHours(2), DepartureTime = DateTime.UtcNow.AddHours(2) },
                new() { FlightNumber = "34567", Price = 17900, Duration = TimeSpan.FromHours(10), DepartureTime = DateTime.UtcNow.AddHours(4) }
            ];

            flights.Sort();
            Console.WriteLine("Economy View:");
            foreach (var flight in flights) Console.WriteLine(flight);

            Console.WriteLine();
            flights.Sort(new DurationComparer());
            Console.WriteLine("Business Runner View");
            foreach (var flight in flights) Console.WriteLine(flight);

            Console.WriteLine();
            flights.Sort(new DepartureComparer());
            Console.WriteLine("Early Bird View");
            foreach (var flight in flights) Console.WriteLine(flight);
        }
    }

    class Flight : IComparable<Flight>
    {
        public string? FlightNumber { get; set; }
        public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime DepartureTime { get; set; }

        public int CompareTo(Flight? other) =>
            Price.CompareTo(other!.Price);

        public override string ToString() =>
            $"{FlightNumber} {Price} {Duration} {DepartureTime}";
    }

    class DurationComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y) =>
            x!.Duration.CompareTo(y!.Duration);
    }

    class DepartureComparer : IComparer<Flight>
    {
        public int Compare(Flight? x, Flight? y) =>
            x!.DepartureTime.CompareTo(y!.DepartureTime);
    }
}
