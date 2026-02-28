namespace FinancialPortfolioManagement;

class Program
{
    static void Main(string[] args)
    {
        Portfolio portfolio = new();

        var equity = new Equity("EQ001", "INFY", "INR", DateTime.Now, 100, 1500, 1650);
        var bond = new Bond("BD001", "GovBond", "INR", DateTime.Now, 50, 1000, 1050);
        var mf = new MutualFund("MF001", "HDFC Fund", "INR", DateTime.Now, 200, 500, 550);

        portfolio.AddInstrument(equity);
        portfolio.AddInstrument(bond);
        portfolio.AddInstrument(mf);

        Transaction[] txArray = [
            new() { Id="T1", InstrumentId="EQ001", Type=TransactionType.Buy, Units=10, Date=DateTime.Now },
            new() { Id="T2", InstrumentId="MF001", Type=TransactionType.Sell, Units=20, Date=DateTime.Now }
        ];

        Transaction.ProcessTransaction(txArray, portfolio);

        ReportGenerator.GenerateConsoleReport(portfolio);
        ReportGenerator.GenerateFileReport(portfolio);
    }
}
