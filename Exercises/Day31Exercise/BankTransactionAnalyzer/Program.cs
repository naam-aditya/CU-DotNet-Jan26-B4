using System.Globalization;

namespace BankTransactionAnalyzer;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo indianCulture = new("en-IN");
        List<Transaction> transactions = [
            new Transaction 
            { 
                AccountNumber = 101, 
                Amount = 5000, 
                Type = TransactionType.CREDIT, 
                Date = DateTime.Now.AddMonths(-3) 
            },
            new Transaction 
            { 
                AccountNumber = 101, 
                Amount = 2000, 
                Type = TransactionType.DEBIT, 
                Date = DateTime.Now 
            },
            new Transaction 
            { 
                AccountNumber = 102, 
                Amount = 10000, 
                Type = TransactionType.DEBIT, 
                Date = DateTime.Now 
            }
        ];

        Console.WriteLine("TOTAL BALANCE PER ACCOUNT:");
        transactions.GroupBy(t => t.AccountNumber)
            .Select(
                g => new 
                { 
                    g.Key, 
                    TotalBalance = g.Sum(
                        t => (t.Type == TransactionType.CREDIT) 
                            ? t.Amount 
                            : -t.Amount
                    ) 
                }
            )
            .ToList()
            .ForEach(r => 
                Console.WriteLine($"Account Number: {r.Key} -> {r.TotalBalance.ToString("C2", indianCulture)}"));
        
        Console.WriteLine("\nSUSPICIOUS ACCOUNTS:");
        transactions.GroupBy(t => t.AccountNumber)
            .Select(
                g => new 
                { 
                    g.Key, 
                    TotalBalance = g.Sum(
                        t => (t.Type == TransactionType.CREDIT) 
                            ? t.Amount 
                            : -t.Amount
                    ) 
                }
            )
            .Where(x => x.TotalBalance < 0)
            .ToList()
            .ForEach(r => Console.WriteLine($"Account Number: {r.Key}"));
        
        Console.WriteLine("\nTRANSACTIONS BY MONTH:");
        transactions.GroupBy(t => t.Date.Month)
            .Select(g => new { g.Key, Transactions = string.Join(" | ", g.ToList())})
            .ToList()
            .ForEach(r => Console.WriteLine($"{r.Key}: [{r.Transactions}]"));
        
        Console.WriteLine("\nHIGHEST TRANSACTION PER ACCOUNT:");
        transactions.GroupBy(t => t.AccountNumber)
            .Select(
                g =>
                {   
                    var HighestTransaction = g.MaxBy(t => t.Amount);
                    return new
                    {
                        g.Key,
                        HighestTransactionAmount = HighestTransaction?.Amount,
                        HighestTransactionType = HighestTransaction?.Type.ToString()
                    };
                }
            )
            .ToList()
            .ForEach(r => 
                Console.WriteLine($"{r.Key}: {r.HighestTransactionAmount?.ToString("C2", indianCulture)} - {r.HighestTransactionType}"));
    }
}

enum TransactionType { CREDIT, DEBIT }
class Transaction
{
    public int AccountNumber { get; set; }
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }

    public override string ToString() =>
        $"{AccountNumber}, {Amount}, {Type}";
}