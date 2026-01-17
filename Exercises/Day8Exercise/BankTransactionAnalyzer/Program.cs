using System.Text;

namespace BankTransactionAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? record = Console.ReadLine();
            if (string.IsNullOrEmpty(record) || string.IsNullOrWhiteSpace(record))
            {
                Console.WriteLine(NonFinancialTransaction());
                return;
            }

            string[] records = record.Split('#');

            if (records.Length != 3)
            {
                Console.WriteLine(NonFinancialTransaction());
                return;
            }

            string transactionId = records[0].Trim();
            string accountHolderName = CustomStringFormat(records[1].Trim());
            string transactionNarration = CustomStringFormat(records[2].Trim()).ToLower();
            string transactionCategory = string.Empty;

            string[] keywords = ["deposit", "withdrawal", "transfer"];
            string transactionKeyword = string.Empty;

            if (transactionNarration.Contains(keywords[0]))
            {
                transactionKeyword = keywords[0];
            }
            else if (transactionCategory.Contains(keywords[1]))
            {
                transactionKeyword = keywords[1];
            }
            else if (transactionCategory.Contains(keywords[2]))
            {
                transactionKeyword = keywords[2];
            }
            else
            {
                Console.WriteLine(NonFinancialTransaction());
                return;
            }

            if (transactionNarration.Equals($"cash {transactionKeyword} successful"))
            {
                transactionCategory = "STANDARD TRANSACTION";
            }
            else
            {
                transactionCategory = "CUSTOM TRANSACTION";
            }

            DisplayOutputFormat(
                    transactionId, 
                    accountHolderName, 
                    transactionNarration, 
                    transactionCategory
                );
        }

        static string NonFinancialTransaction()
        {
            return "NON-FINANCIAL TRANSACTION";
        }

        static string CustomStringFormat(string unFrormatedString)
        {
            StringBuilder stringBuilder = new();
            string[] stringRecord = 
                unFrormatedString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < stringRecord.Length; i++)
            {
                stringBuilder.Append(stringRecord[i]);
                stringBuilder.Append(' ');
            }

            return stringBuilder.ToString().Trim();
        }

        static void DisplayOutputFormat(
            string transactionId, 
            string accountHolderName, 
            string transactionNarration, 
            string transactionCategory
        )
        {
            Console.WriteLine($"{"Transaction ID", -15}: {transactionId}");
            Console.WriteLine($"Account Holder : {accountHolderName}");
            Console.WriteLine($"{"Narration", -15}: {transactionNarration}");
            Console.WriteLine($"{"Category", -15}: {transactionCategory}");
        }
    }
}
