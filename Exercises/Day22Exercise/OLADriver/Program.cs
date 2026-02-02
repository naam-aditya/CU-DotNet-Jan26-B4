using System.Text;

namespace OLADriver
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Driver> drivers = [
                new() { Id = "12345", Name = "Suresh", VehicleNumber = "BH09A2345" },
                new() { Id = "12346", Name = "Mukesh", VehicleNumber = "BH09A2345" },
                new() { Id = "12347", Name = "Ramesh", VehicleNumber = "BH09A2345" },
                new() { Id = "12348", Name = "Mahesh", VehicleNumber = "BH09A2345" }
            ];

            drivers[0].AddRides([
                new() { Id = "1", From = "Mohali", To = "Karachi", Fare = 30000.0m }, 
                new() { Id = "2", From = "Sonipat", To = "Delhi", Fare = 4000.0m }, 
                new() { Id = "3", From = "Ambala", To = "Patiala", Fare = 3000.0m },  
            ]);

            drivers[1].AddRides([
                new() { Id = "1", From = "Mohali", To = "Karachi", Fare = 30000.0m }, 
                new() { Id = "2", From = "Sonipat", To = "Delhi", Fare = 4000.0m }, 
                new() { Id = "3", From = "Ambala", To = "Patiala", Fare = 3000.0m },  
            ]);

            drivers[2].AddRides([
                new() { Id = "1", From = "Mohali", To = "Karachi", Fare = 30000.0m }, 
                new() { Id = "2", From = "Sonipat", To = "Delhi", Fare = 4000.0m }, 
                new() { Id = "3", From = "Ambala", To = "Patiala", Fare = 3000.0m },  
            ]);

            drivers[3].AddRides([
                new() { Id = "1", From = "Mohali", To = "Karachi", Fare = 30000.0m }, 
                new() { Id = "2", From = "Sonipat", To = "Delhi", Fare = 4000.0m }, 
                new() { Id = "3", From = "Ambala", To = "Patiala", Fare = 3000.0m },  
            ]);

            foreach (var driver in drivers)
                Console.WriteLine(driver);
        }
    }

    class Ride
    {
        public required string Id { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public required decimal Fare { get; set; }

        public override string ToString() =>
            $"[Id: {Id}, From: {From}, To: {To}, Fare: {Fare:N2}]";
    }

    class Driver
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string VehicleNumber { get; set; }
        public List<Ride> Rides { get; private set; } = [];

        public void AddRide(Ride ride) =>
            Rides.Add(ride);
        
        public void AddRides(List<Ride> rides) =>
            Rides.AddRange(rides);

        public override string ToString() =>
            $"Id: {Id}, Name: {Name}, Vehicle Number: {VehicleNumber} Total Fare: {GetTotalFare()}\nRides:\n{GetAllRides()}";
        
        private string GetAllRides()
        {
            StringBuilder stringBuilder = new();
            foreach (var ride in Rides)
            {
                stringBuilder.Append(ride.ToString());
                stringBuilder.Append('\n');
            }
            return stringBuilder.ToString();
        }

        private string GetTotalFare()
        {
            decimal total = 0m;
            foreach (var ride in Rides)
                total += ride.Fare;
            return total.ToString("N2");
        }
    }
}