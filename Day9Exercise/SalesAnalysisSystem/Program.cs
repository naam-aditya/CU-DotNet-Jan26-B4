namespace SalesAnalysisSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] salesCategory = new string[7];
            decimal[] dailySales = new decimal[7];
            decimal totalWeeklySale = 0;

            for (int i = 0; i < dailySales.Length; i++)
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

                if (todaysSale < 5000)
                {
                    salesCategory[i] = "Low";
                }

                else if (todaysSale >= 5000 && todaysSale <= 15000) {
                    salesCategory[i] = "Medium";
                }

                else
                {
                    salesCategory[i] = "High";
                }

                dailySales[i] = todaysSale;
                totalWeeklySale += todaysSale;
            }

            decimal averageSale = totalWeeklySale / dailySales.Length;
            decimal aboveAverageSalesCount = 0;
            decimal highestSaleDay = 0;
            decimal lowestSaleDay = 0;

            decimal highestSale = 0;
            decimal lowestSale = decimal.MaxValue;

            for (int i = 0; i < dailySales.Length; i++)
            {
                if (dailySales[i] > averageSale)
                {
                    aboveAverageSalesCount++;
                }

                if (highestSale < dailySales[i])
                {
                    highestSale = dailySales[i];
                    highestSaleDay = i + 1;
                }

                if (lowestSale > dailySales[i])
                {
                    lowestSale = dailySales[i];
                    lowestSaleDay = i + 1;
                }
            }

            // -------- Output Format

            Console.WriteLine("\nWeekly Sales Report\n" + 
                              "--------------------\n" + 
                             $"{"Total Sales", -19}: {totalWeeklySale.ToString("N2")}\n" +
                             $"{"Average Daily Sale", -19}: {averageSale.ToString("N2")}\n" +
                             $"{"Highest Sale", -19}: {highestSale.ToString("N2")} (Day {highestSaleDay})\n" +
                             $"{"Lowest Sale", -19}: {lowestSale.ToString("N2")} (Day {lowestSaleDay})\n\n" +
                             $"Days Above Average: {aboveAverageSalesCount}");
            
            for (int i = 0; i < salesCategory.Length; i++)
            {
                Console.WriteLine($"Day {i + 1} : {salesCategory[i]}");
            }
        }
    }
}
