namespace FinancialPortfolioManagement;

interface IRiskAssessable
{
    string GetRiskCategory();
}

interface IReportable
{
    string GenerateReportLine();
}