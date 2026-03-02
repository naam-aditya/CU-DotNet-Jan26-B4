using System.Text;

namespace SaasArchitect;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, Subscriber> subscribers = [];

        subscribers.Add("corp1@company.com",
                new BusinessSubscriber("Tech Corp", new DateTime(2023, 1, 15), 1000m, 0.15m));

            subscribers.Add("corp2@enterprise.com",
                new BusinessSubscriber("Enterprise Ltd", new DateTime(2022, 10, 5), 2000m, 0.18m));

            subscribers.Add("john@email.com",
                new ConsumerSubscriber("John Doe", new DateTime(2023, 3, 10), 50m, 2m));

            subscribers.Add("anna@email.com",
                new ConsumerSubscriber("Anna Smith", new DateTime(2022, 11, 20), 75m, 1.8m));

            subscribers.Add("startup@biz.com",
                new BusinessSubscriber("Startup Inc", new DateTime(2023, 5, 1), 1500m, 0.10m));

            var sortedByBillDesc = subscribers
                .OrderByDescending(s => s.Value.CalculateMonthlyBill())
                .Select(s => s.Value)
                .ToList();

            ReportGenerator.PrintRevenueReport(sortedByBillDesc);
    }
}


public abstract class Subscriber(string name, DateTime joinDate) : IComparable<Subscriber>
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public DateTime JoinDate { get; set; } = joinDate;

    public abstract decimal CalculateMonthlyBill();

    public int CompareTo(Subscriber? other)
    {
        if (other == null) return 1;
        
        int dateComaprision = JoinDate.CompareTo(other.JoinDate);

        if (dateComaprision == 0)
            return string.Compare(
                Name, 
                other.Name, 
                StringComparison.OrdinalIgnoreCase
            );
        
        return dateComaprision;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Subscriber other)
            return Id == other.Id;
        
        return false;
    }

    public override int GetHashCode() =>
        Id.GetHashCode();
}

class BusinessSubscriber(
    string name, DateTime joinDate, decimal fixedRate, decimal taxRate
) : Subscriber(name, joinDate)
{
    public decimal FixedRate { get; set; } = fixedRate;
    public decimal TaxRate { get; set; } = taxRate;

    public override decimal CalculateMonthlyBill() =>
        FixedRate * (1 + TaxRate);
}

class ConsumerSubscriber(
    string name, DateTime joinDate, decimal dataUsageGB, decimal pricePerGB
) : Subscriber(name, joinDate)
{
    public decimal DataUsageGB { get; set; } = dataUsageGB;
    public decimal PricePerGB { get; set; } = pricePerGB;

    public override decimal CalculateMonthlyBill() =>
        DataUsageGB * PricePerGB;
}

public static class ReportGenerator
{
    public static void PrintRevenueReport(IEnumerable<Subscriber> subscribers)
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine("MONTHLY REVENUE REPORT");
        stringBuilder.AppendLine($"{"Type",-12} {"Name",-20} {"Join Date",-15} {"Monthly Bill",15}");

        foreach (var subscriber in subscribers)
        {
            string type = subscriber is BusinessSubscriber ? "Business" : "Consumer";
            decimal bill = subscriber.CalculateMonthlyBill();

            stringBuilder.AppendLine(
                $"{type,-12} {subscriber.Name,-20} {subscriber.JoinDate.ToShortDateString(),-15} {bill,15:C}"
            );
        }

        Console.WriteLine(stringBuilder.ToString());
    }
}
