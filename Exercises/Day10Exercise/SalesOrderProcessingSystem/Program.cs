namespace SalesOrderProcessingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] categories = new string[7];
            decimal[] sales = new decimal[7];

            ReadWeeklySales(sales);
            GenerateSalesCategory(sales, categories);

            decimal totalOfSales = CalculateTotal(sales);
            decimal averageOfSales = CalculateAverage(totalOfSales, sales.Length);

            decimal highestSale = FindHighestSale(sales, out int highestSaleDay);
            decimal lowestSale = FindLowestSale(sales, out int lowestSaleDay);

            decimal discount = CalculateDiscount(totalOfSales);
            decimal tax = CalculateTax(totalOfSales - discount);
            decimal finalPayableAmount = CalculateFinalAmount(totalOfSales, discount, tax);

            // -------- Output Format

            Console.WriteLine(
                "\nWeekly Sales Summary\n" + 
                "--------------------\n" + 
                $"{"Total Sales", -19}: {totalOfSales.ToString("N2")}\n" +
                $"{"Average Daily Sale", -19}: {averageOfSales.ToString("N2")}\n\n" +
                $"{"Highest Sale", -13}: {highestSale.ToString("N2")} (Day {highestSaleDay})\n" +
                $"{"Lowest Sale", -13}: {lowestSale.ToString("N2")} (Day {lowestSaleDay})\n\n" +
                $"Discount Applied : {discount.ToString("N2")}\n" +
                $"{"Tax Amount", -17}: {tax.ToString("N2")}\n" +
                $"{"Final Payable", -17}: {finalPayableAmount.ToString("N2")}\n\n" +
                "Day-wise Category:\n"
            );
            
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"Day {i + 1} : {categories[i]}");
            }
        }

        static void ReadWeeklySales(decimal[] sales)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                Console.Write($"Day {i + 1} : ");
                decimal todaysSale = Convert.ToDecimal(Console.ReadLine());

                while (todaysSale < 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("PLEASE ENTER A VALID VALUE");
                    Console.Write($"Day {i + 1} : ");
                    todaysSale = Convert.ToDecimal(Console.ReadLine());
                }

                sales[i] = todaysSale;
            }
        }

        static decimal CalculateTotal(decimal[] sales)
        {
            return sales.Sum();
        }

        static decimal CalculateAverage(decimal total, int days)
        {
            return total / days;
        }

        static decimal FindHighestSale(decimal[] sales, out int day)
        {
            decimal highestSale = 0;
            day = 0;

            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] > highestSale)
                {
                    highestSale = sales[i];
                    day = i + 1;
                }
            }

            return highestSale;
        }

        static decimal FindLowestSale(decimal[] sales, out int day)
        {
            decimal lowestSale = decimal.MaxValue;
            day = 0;

            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] < lowestSale)
                {
                    lowestSale = sales[i];
                    day = i + 1;
                }
            }

            return lowestSale;
        }

        static decimal CalculateDiscount(decimal total)
        {
            if (total >= 50000)
            {
                return total / 10;
            }
            
            else
            {
                return total / 20;
            }
        }

        static decimal CalculateDiscount(decimal total, bool isFestivalWeek)
        {
            if (!isFestivalWeek)
            {
                return CalculateDiscount(total);
            }

            if (total >= 50000)
            {
                return total * 3 / 20;
            }
            
            else
            {
                return total / 10;
            }
        }

        static decimal CalculateTax(decimal amount)
        {
            return amount * 18 / 100;
        }

        static decimal CalculateFinalAmount(decimal total, decimal discount, decimal tax)
        {
            return total - discount + tax;
        }

        static void GenerateSalesCategory(decimal[] sales, string[] categories)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] < 5000)
                {
                    categories[i] = "Low";
                }

                else if (sales[i] >= 5000 && sales[i] <= 15000)
                {
                    categories[i] = "Medium";
                }

                else
                {
                    categories[i] = "High";
                }
            }
        }
    }
}
