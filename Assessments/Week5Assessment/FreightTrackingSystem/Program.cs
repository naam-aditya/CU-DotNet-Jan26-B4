namespace FreightTrackingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Shipment> shipments = [
                new ExpressShipment(
                    trackingId: "K1234",
                    weight: 450.0,
                    destination: "Chandigarh"
                ),
                new HeavyFreight(
                    trackingId: "K1235",
                    weight: 1100.0,
                    destination: "Mohali"
                ),
                new ExpressShipment(
                    trackingId: "K1236",
                    weight: -23.0,
                    destination: "Nepal"
                ),
                new HeavyFreight(
                    trackingId: "K1237",
                    weight: 1400.0,
                    destination: "Russia",
                    permit: "Heavy Lift"
                ),
                new ExpressShipment(
                    trackingId: "K1238",
                    weight: 340.0,
                    destination: "Pakistan"
                ),
                new ExpressShipment(
                    trackingId: "K1239",
                    weight: 340.0,
                    destination: "Mumbai",
                    isFragile: true
                ),
            ];

            foreach (var shipment in shipments)
            {
                LogManager manager = new();
                try
                {
                    shipment.ProcessShipment();
                    manager.SaveLog($"SUCCESS: {shipment.TrackingId}\n");
                }
                catch (RestrictedDestinationException ex)
                {
                    manager.SaveLog($"FAILED: {shipment.TrackingId}\n{ex.Message}\n");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    manager.SaveLog($"FAILED: {shipment.TrackingId}\n{ex.Message}\n");
                }
                catch (InsecurePackagingException ex)
                {
                    manager.SaveLog($"FAILED: {shipment.TrackingId}\n{ex.Message}\n");
                }
                catch (Exception ex)
                {
                    manager.SaveLog($"FAILED: {shipment.TrackingId}\nUnhandled Exception Occured\n{ex.Message}\n");
                }
                finally
                {
                    Console.WriteLine($"Processing attempt finished for ID: {shipment.TrackingId}");
                }
            }
        }
    }

    abstract class Shipment(
        string trackingId, double weight, string destination
    )
    {
        public string TrackingId { get; set; } = trackingId;
        public double Weight { get; set; } = weight;
        public string Destination { get; set; } = destination;

        public abstract void ProcessShipment();

        public bool IsDestinationRestricted()
        {
            List<string> restrictedDestinations = [
              "North Pole",
              "Epstein Island",
              "Pakistan",
              "Mars",
              "Wakanda"  
            ];

            return restrictedDestinations.Contains(Destination);
        }
    }

    class ExpressShipment(
        string trackingId, 
        double weight, 
        string destination,
        bool isFragile = false,
        string? status = null
    ) : Shipment(trackingId, weight, destination)
    {
        public bool? IsFragile { get; set; } = isFragile;
        public string? Status { get; set; } = status;
        
        public override void ProcessShipment()
        {
            if (IsDestinationRestricted())
                throw new RestrictedDestinationException($"Restricted Destination: {Destination}.");
            
            if (Weight <= 0)
                throw new ArgumentOutOfRangeException("Data Entry Error: Invalid Weight.");
            
            if (IsFragile == true && Status != "Reinforced")
                throw new InsecurePackagingException("Insecure Packaging.");

        }
    }

    class HeavyFreight(
        string trackingId, 
        double weight, 
        string destination,
        string? permit = null
    ) : Shipment(trackingId, weight, destination)
    {
        public string? Permit { get; set; } = permit;
        public override void ProcessShipment()
        {
            if (IsDestinationRestricted())
                throw new RestrictedDestinationException("RESTRICTED DESTINATION");
            
            if (Weight > 1000 && Permit != "Heavy Lift")
                throw new ArgumentOutOfRangeException("Data Entry Error: Invalid Permit");
        }
    }

    interface ILoggable
    {
        public void SaveLog(string message);
    }

    class LogManager : ILoggable
    {
        public void SaveLog(string message)
        {
            string fileName = "shipment_audit.log";
            using var writer = new StreamWriter(fileName, true);

            writer.WriteLine(message);
        }
    }

    class RestrictedDestinationException(string message) : Exception(message) { }
    class InsecurePackagingException(string message) : Exception(message) { }
}