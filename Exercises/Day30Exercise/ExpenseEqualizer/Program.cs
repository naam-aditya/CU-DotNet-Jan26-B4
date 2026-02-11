namespace ExpenseEqualizer;

class Program
{
    static void Main(string[] args)
    {
        List<Tuple<string, double>> expenses = [
            new Tuple<string, double>("Aman", 900),
            new Tuple<string, double>("Soman", 0),
            new Tuple<string, double>("Kartik", 1290),
        ];

        CalculateAndExportExpenses(expenses);
    }

    static void CalculateAndExportExpenses(List<Tuple<string, double>> expenses)
    {
        double average = expenses.Average((x) => x.Item2);
        var payments =
            expenses.Select(x => new Tuple<string, double>(x.Item1, average - x.Item2))
                .OrderBy(x => x.Item2)
                .ToList();

        int beg = 0;
        int end = payments.Count - 1;

        List<string> result = [];

        while (beg <= end)
        {
            if (Math.Abs(payments[beg].Item2) > Math.Abs(payments[end].Item2))
            {
                result.Add($"{payments[end].Item1},{payments[beg].Item1},{payments[end].Item2}");
                payments[beg] = new(
                    payments[beg].Item1,
                    payments[beg].Item2 + payments[end].Item2
                );
                payments[end] = new(payments[end].Item1, 0);
            }
            else if (Math.Abs(payments[beg].Item2) < Math.Abs(payments[end].Item2))
            {
                result.Add($"{payments[end].Item1},{payments[beg].Item1},{Math.Abs(payments[beg].Item2)}");
                payments[end] = new(
                    payments[end].Item1,
                    payments[end].Item2 + payments[beg].Item2
                );
                payments[beg] = new(payments[beg].Item1, 0);
            }
            else
            {
                result.Add($"{payments[end].Item1},{payments[beg].Item1},{Math.Abs(payments[beg].Item2)}");
                payments[beg] = new(payments[beg].Item1, 0);
                payments[end] = new(payments[end].Item1, 0);
            }

            if (payments[beg].Item2 == 0)
                beg++;

            if (payments[end].Item2 == 0)
                end--;
        }

        using StreamWriter streamWriter = new("settlement.csv");
        streamWriter.WriteLine("Payer,Receiver,Amount");
        result.ForEach(x => streamWriter.WriteLine(x));
    }
}
