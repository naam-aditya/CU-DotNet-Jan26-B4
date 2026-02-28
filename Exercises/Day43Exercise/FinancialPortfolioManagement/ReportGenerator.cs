namespace FinancialPortfolioManagement;

class ReportGenerator
{
    public static void GenerateConsoleReport(Portfolio portfolio)
    {
        Console.WriteLine("PORTFOLIO SUMMARY\n");

        var grouped = portfolio.GetAllInstruments()
            .GroupBy(t => t.GetType().Name);
        
        foreach (var group in grouped)
        {
            decimal totalInvestment = group.Sum(t => t.Units * t.PurchasePrice);
            decimal currentValue = group.Sum(t => t.CalculateCurrentValue());

            Console.WriteLine($"Instument Type: {group.Key}");
            Console.WriteLine($"Total Investment: {totalInvestment:C2}");
            Console.WriteLine($"Current Value: {currentValue:C2}");
            Console.WriteLine($"Profit/Loss: {(currentValue - totalInvestment):C2}\n");
        }

        Console.WriteLine($"Overall Portfolio Value: {portfolio.GetTotalPortfolioValue():C2}");

        var riskDist = portfolio.GetAllInstruments()
            .OfType<IRiskAssessable>()
            .GroupBy(t => t.GetRiskCategory());
        
        Console.WriteLine("\nRisk Distribution:");
        foreach (var risk in riskDist)
            Console.WriteLine($"{risk.Key}: {risk.Count()}");
    }

    public static void GenerateFileReport(Portfolio portfolio)
    {
        string fileName = $"PortfolioReport_{DateTime.Now:yyyyMMdd}.txt";

        try
        {
            using StreamWriter writer = new(fileName);
            writer.WriteLine("PORTFOLIO REPORT");
            writer.WriteLine($"Generated: {DateTime.Now}\n");

            foreach (var inst in portfolio.GetAllInstruments())
                writer.WriteLine(inst.GetInstrumentSummary());

            writer.WriteLine($"\nTotal Portfolio Value: {portfolio.GetTotalPortfolioValue():C}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"File Write Error: {ex.Message}");
        }
    }
}