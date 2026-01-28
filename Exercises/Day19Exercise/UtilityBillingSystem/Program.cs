namespace UtilityBillingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<UtilityBill> bills = [
                new ElectricityBill(1, "Aditya", 320, 4),
                new WaterBill(2, "Aditya", 140, 4),
                new GasBill(3, "Aditya", 200, 4)
            ];

            foreach (var bill in bills)
                bill.PrintBill();
        }
    }

    abstract class UtilityBill(
        int consumerId = 1, 
        string consumerName = "", 
        decimal unitsConsumed = 0, 
        decimal ratePerUnit = 0
    )
    {
        public int ConsumerId { get; set; } = consumerId;
        public string ConsumerName { get; set; } = consumerName;
        public decimal UnitsConsumed { get; set; } = unitsConsumed;
        public decimal RatePerUnit { get; set; } = ratePerUnit;

        public abstract decimal CalculateBillAmount();

        public virtual decimal CalculateTax(decimal billAmount) =>
            billAmount * 0.05m;

        public void PrintBill()
        {
            decimal finalAmount = 
                CalculateBillAmount() + CalculateTax(UnitsConsumed * RatePerUnit);
            Console.WriteLine(
                $"Id: {ConsumerId}, Name: {ConsumerName}, " + 
                $"Total Units: {UnitsConsumed}, " +
                $"Final Amount: {finalAmount:N2}"
            );
        }
    }

    class ElectricityBill(
        int consumerId, 
        string consumerName, 
        decimal unitsConsumed, 
        decimal ratePerUnit
    ) : UtilityBill(consumerId, consumerName, unitsConsumed, ratePerUnit)
    {
        public override decimal CalculateBillAmount() =>
            (UnitsConsumed > 300) 
                ? UnitsConsumed * RatePerUnit + (UnitsConsumed * RatePerUnit * 0.10m)
                : UnitsConsumed * RatePerUnit;
    }

    class WaterBill(
        int consumerId, 
        string consumerName, 
        decimal unitsConsumed, 
        decimal ratePerUnit
    ) : UtilityBill(consumerId, consumerName, unitsConsumed, ratePerUnit)
    {
        public override decimal CalculateBillAmount() =>
            UnitsConsumed * RatePerUnit;

        public override decimal CalculateTax(decimal billAmount) =>
            billAmount * 0.02m;
    }

    class GasBill(
        int consumerId, 
        string consumerName, 
        decimal unitsConsumed, 
        decimal ratePerUnit
    ) : UtilityBill(consumerId, consumerName, unitsConsumed, ratePerUnit)
    {
        public override decimal CalculateBillAmount() =>
            UnitsConsumed * RatePerUnit + 150;

        public override decimal CalculateTax(decimal billAmount) => 0;
    }
}