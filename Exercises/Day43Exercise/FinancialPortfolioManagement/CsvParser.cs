namespace FinancialPortfolioManagement;

class CsvParser
    {
        public static FinancialInstrument ParseInstrument(string csvLine)
        {
            string[] parts = csvLine.Split(',');

            if (parts.Length != 7)
                throw new Exception("Invalid CSV format.");

            string id = parts[0];
            string type = parts[1];
            string name = parts[2];
            string currency = parts[3];
            decimal units = decimal.Parse(parts[4]);
            decimal purchase = decimal.Parse(parts[5]);
            decimal market = decimal.Parse(parts[6]);

            return type switch
            {
                "Equity" => new Equity(id, name, currency, DateTime.Now, units, purchase, market),
                "Bond" => new Bond(id, name, currency, DateTime.Now, units, purchase, market),
                "FixedDeposit" => new FixedDeposit(id, name, currency, DateTime.Now, units, purchase, market),
                "MutualFund" => new MutualFund(id, name, currency, DateTime.Now, units, purchase, market),
                _ => throw new Exception("Invalid instrument type.")
            };
        }
    }