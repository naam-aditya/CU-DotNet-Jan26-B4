namespace SmartKitchen
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Appliance> kitchenAppliances = [
                new Microwave("Sumsang Micro Ultra", 1200),
                new ElectricOven("Sanaponic Pro Oven", 2400),
                new AirFryer("Abibas Mini Fryer", 1500)
            ];

            foreach (var appliance in kitchenAppliances)
            {
                appliance.Cook();
                if (appliance is ITimer timer)
                    timer.SetTimer(100);
                
                if (appliance is ISmart smart)
                    smart.ConnectToWifi();
                
                Console.WriteLine();
            }
        }
    }

    interface ITimer
    {
        void SetTimer(int minutes);
    }

    interface ISmart
    {
        void ConnectToWifi();
    }

    abstract class Appliance
    {
        public string? ModelName { get; set; }
        public int PowerConsumption { get; set; }

        protected Appliance(string modelName, int powerConsumption)
        {
            ModelName = modelName;
            PowerConsumption = powerConsumption;
        }

        public abstract void Cook();

        public virtual void PreHeat()
        {
            Console.WriteLine("Heating");
        }
    }

    class Microwave(string modelName, int powerConsumption) 
        : Appliance(modelName, powerConsumption), ITimer
    {
        public override void Cook() =>
            Console.WriteLine($"{ModelName}: Cooking.");

        public void SetTimer(int minutes) =>
            Console.WriteLine($"{ModelName}: Timer set for {minutes} minutes.");
    }

    class ElectricOven(string modelName, int powerConsumption)
        : Appliance(modelName, powerConsumption), ITimer, ISmart
    {
        public void ConnectToWifi() =>
            Console.WriteLine($"{ModelName}: Connected to Wi-Fi network.");

        public override void Cook() =>
            Console.WriteLine($"{ModelName}: Cooking.");

        public void SetTimer(int minutes) =>
            Console.WriteLine($"{ModelName}: Timer set for {minutes} minutes.");

        public override void PreHeat() =>
            Console.WriteLine($"{ModelName}: Preheating....");
    }

    class AirFryer(string modelName, int powerConsumption)
        : Appliance(modelName, powerConsumption)
    {
        public override void Cook() =>
            Console.WriteLine($"{ModelName}: Cooking.");
    }
}