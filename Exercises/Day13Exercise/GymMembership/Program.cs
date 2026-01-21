namespace Day13Exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool[] services = OptedGymServices();
            Console.WriteLine(CalculateTotalMembershipAmount(services).ToString("N2"));
        }

        static bool[] OptedGymServices()
        {
            bool[] services = new bool[3];
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return services;
            }

            string[] inputs = input.Split('#');

            for (byte i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == "1")
                {
                    services[i] = true;
                }
            }

            return services;
        }

        static decimal CalculateTotalMembershipAmount(bool[] services)
        {
            decimal totalAmount = 1000;
            if (services.Length != 3)
            {
                return -1;
            }
    
            if (services[0] || services[1] || services[2])
            {
                if (services[0])
                {
                    totalAmount += 300;
                }

                if (services[1])
                {
                    totalAmount += 500;
                }

                if (services[2])
                {
                    totalAmount += 250;
                }
            }
            else
            {
                totalAmount += 200;
            }

            totalAmount += totalAmount / 20;
            return totalAmount;
        }
    }
}
