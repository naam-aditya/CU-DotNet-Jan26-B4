namespace FinancialPortfolioManagement;

enum TransactionType { Buy, Sell }

class Transaction
{
    public required string Id { get; set; }
    public required string InstrumentId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Units { get; set; }
    public DateTime Date { get; set; }

    public static void ProcessTransaction(Transaction[] transactionArray, Portfolio portfolio)
    {
        List<Transaction> transactions = transactionArray.ToList();
        foreach (var tx in transactions)
        {
            var instrument = portfolio.GetInstrumentById(tx.InstrumentId) 
                    ?? throw new Exception("Intrument not found.");
                
            if (tx.Type == TransactionType.Buy)
                instrument.Units += tx.Units;
            else
            {
                if (tx.Units > instrument.Units)
                    throw new Exception("Cannot sell more than owned");
                instrument.Units -= tx.Units;
            }
        }
    }
}
