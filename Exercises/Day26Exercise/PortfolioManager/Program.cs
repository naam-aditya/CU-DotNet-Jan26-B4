namespace PortfolioManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "loansdb.csv";
            do
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    break;
                
                CSVFileHandler.AppendToFile(filePath, input);
            }
            while (true);

            List<Loan> loans = CSVFileHandler.ReadFromFile(filePath);
            
            Console.WriteLine(CSVFileHandler.GetDisplayHeaderLine());
            Console.WriteLine(new string('-', 53));
            foreach (var item in loans)
                Console.WriteLine(
                    $"{item.ClientName,-10} | " +
                    $"{item.Principal,12:C2} | " +
                    $"{item.CalculateInterestAmount(),12:C2} | " +
                    $"{item.GetRiskLevel()}"
                );
        }
    }

    class Loan(string clientName, double principal, double interestRate)
    {
        public string ClientName { get; set; } = clientName;
        public double Principal { get; set; } = principal;
        public double InterestRate { get; set; } = interestRate;

        public double CalculateInterestAmount() =>
            Principal * InterestRate / 100.0;

        public string GetRiskLevel()
        {
            if (InterestRate > 10)
                return "HIGH";
            else if (InterestRate >= 5 && InterestRate <= 10)
                return "MEDIUM";
            else
                return "LOW";
        }
    }

    static class CSVFileHandler
    {
        public static bool AppendToFile(string filePath, string data)
        {
            try
            {
                using StreamWriter streamWriter = new(filePath, true);
                streamWriter.WriteLine(data);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while appending data: {ex.Message}");
            }
            return false;
        }

        public static List<Loan> ReadFromFile(string filePath)
        {
            List<Loan> loans = [];

            try
            {
                using StreamReader streamReader = new(filePath);
                string? line = streamReader.ReadLine();

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if (data.Length != 3)
                        throw new IndexOutOfRangeException();
                    
                    string name = data[0];
                    if (
                        !double.TryParse(data[1], out double principal) ||
                        !double.TryParse(data[2], out double interestRate)
                    )
                        throw new FormatException();

                    loans.Add(new(name, principal, interestRate));
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("\nINVALID DATA FORMAT FOUND\n.");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nINVALID DATA FORMAT FOUND\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error Occured: {ex.Message}.");
            }
            return loans;
        }

        public static string GetDisplayHeaderLine() =>
            $"{"CLIENT",-10} | {"PRINCIPAL",12} | {"INTEREST",12} | {"RISK LEVEL"}";
    }
}