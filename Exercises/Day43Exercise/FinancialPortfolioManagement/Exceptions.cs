namespace FinancialPortfolioManagement;

class InvalidFinancialDataException : Exception
{
    public InvalidFinancialDataException() : base() { }
    public InvalidFinancialDataException(string message) : base(message) { }
}