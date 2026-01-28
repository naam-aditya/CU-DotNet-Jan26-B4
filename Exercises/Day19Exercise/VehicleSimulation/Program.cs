namespace VehicleSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle[] vehicles =
            [
                new ElectricCar("Mahindra"),
                new HeavyTruck("Tata"),
                new CargoPlane("Air India")
            ];

            for (int i = 0; i < vehicles.Length; i++)
            {
                vehicles[i].Move();
                Console.WriteLine(vehicles[i].GetFuelStatus());
                Console.WriteLine();
            }
        }
    }

    abstract class Vehicle(string modelName)
    {
        public string ModelName { get; } = modelName;

        public abstract void Move();

        public virtual string GetFuelStatus() =>
            "Fuel level is stable.";
    }

    class ElectricCar(string modelName) : Vehicle(modelName)
    {
        public override void Move() =>
            Console.WriteLine($"{ModelName} is gliding silently on battery power.");

        public override string GetFuelStatus() =>
            $"{ModelName} battery is at 80%.";
    }

    class HeavyTruck(string modelName) : Vehicle(modelName)
    {
        public override void Move() =>
            Console.WriteLine($"{ModelName} is hauling cargo with high-torque diesel power.");
    }

    class CargoPlane(string modelName) : Vehicle(modelName)
    {
        public override void Move() =>
            Console.WriteLine($"{ModelName} is ascending to 30,000 feet.");

        public override string GetFuelStatus() =>
            $"{base.GetFuelStatus()} Checking jet fuel reserves...";
    }
}
