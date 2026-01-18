using System.Data.Common;

namespace InsuranceSummarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] policyHolderNames = new string[5];
            decimal[] annualPremiums = new decimal[5];

            Console.WriteLine(InputFormat());
            for (int i = 0; i < policyHolderNames.Length; i++)
            {
                string data = TakeValidInput();

                string[] inputs;
                while (!IsInputValid(data, out inputs))
                {
                    data = TakeValidInput();
                }

                policyHolderNames[i] = inputs[0];
                annualPremiums[i] = Convert.ToDecimal(inputs[1]);    
            }

            DisplayOutput(policyHolderNames, annualPremiums);
            Console.WriteLine();
        }

        static string TakeValidInput()
        {
            string? data = string.Empty;
            Console.Write("=> ");
            data = Console.ReadLine();

            while (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
            {
                Console.WriteLine("\nPLEASE ENTER VALID VALUE");    
                Console.Write("=> ");
                data = Console.ReadLine();
            }

            return data;
        }

        static bool IsInputValid(string data, out string[] inputs)
        {
            inputs = data.Split(':');

            if (inputs.Length != 2)
            {
                Console.WriteLine(InputFormat());
                return false;
            }

            if (
                string.IsNullOrWhiteSpace(inputs[0]) ||
                string.IsNullOrEmpty(inputs[0])
            )
            {
                Console.WriteLine(InputFormat());
                return false;
            }

            if (
                string.IsNullOrEmpty(inputs[1]) ||
                string.IsNullOrWhiteSpace(inputs[1])
            )
            {
                Console.WriteLine("\nPLEASE ENTER VALID AMOUNT");
                return false;
            }

            if (decimal.TryParse(inputs[1], out decimal premium))
            {
                if (premium <= 0)
                {
                    Console.WriteLine("\nENTER AMOUNT GREATER THAN 0");
                }
                return true;
            }

            Console.WriteLine("\nSOMETHING WENT WRONG");
            return false;
        }

        static decimal CalculateTotalPremiumAmount(decimal[] annualPremium)
        {
            return annualPremium.Sum();
        }

        static decimal CalculateAveragePremiumAmount(decimal[] annualPremium)
        {

            return CalculateTotalPremiumAmount(annualPremium) / annualPremium.Length;
        }

        static decimal CalculateHighestPremium(decimal[] annualPremium)
        {
            return annualPremium.Max();
        }

        static decimal CalculateLowestPremium(decimal[] annualPremium)
        {
            return annualPremium.Min();
        }

        static string InputFormat()
        {
            return "\nENTER INPUT IN THIS FORMAT - <NAME>:<AMOUNT>";
        }

        static string GetPremiumCategory(decimal premiumAmount)
        {
            if (premiumAmount < 10000)
            {
                return "LOW";
            }
            else if (premiumAmount >= 10000 && premiumAmount <= 25000)
            {
                return "MEDIUM";
            }
            else
            {
                return "HIGH";
            }
        }

        static string DataOutputFormat(string name, decimal premium)
        {
            return $"{name.ToUpper(), -12} {premium, -16:N2} {GetPremiumCategory(premium)}";
        }

        static void DisplayOutput(string[] policyHolderNames, decimal[] annualPremiums)
        {
            Console.Write(
                "\nInsurance Premium Summary" + 
                "\n-------------------------\n" + 
                $"{"Name", -12} {"Premium", -16} {"Category"}\n" +
                "--------------------------------------\n"
            );

            for (int i = 0; i < policyHolderNames.Length; i++)
            {
                Console.WriteLine(
                    $"{DataOutputFormat(policyHolderNames[i], annualPremiums[i])}"
                );
            }

            Console.Write(
                "--------------------------------------\n" +
                $"{"Total Premium", -16}: {CalculateTotalPremiumAmount(annualPremiums).ToString("N2")}\n" +
                $"{"Average Premium", -16}: {CalculateAveragePremiumAmount(annualPremiums).ToString("N2")}\n" +
                $"{"Highest Premium", -16}: {CalculateHighestPremium(annualPremiums).ToString("N2")}\n" +
                $"{"Lowest Premium", -16}: {CalculateLowestPremium(annualPremiums).ToString("N2")}\n"
            );
        }
    }
}