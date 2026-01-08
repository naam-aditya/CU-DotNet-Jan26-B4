
namespace Day2Exercise
{
    internal class Exercises
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static void Exercise1()
        {
            int totalNumberOfDays = 120;
            int presentDays = 79;

            double attendancePercentage = ((double) presentDays / (double) totalNumberOfDays) * 100;
            Console.WriteLine("Rounded Attendance: " + Convert.ToInt32(attendancePercentage));
            Console.WriteLine("Truncated Attendance: " + (int) attendancePercentage);

            /*
                Explanation:

                 - Rounding may increase the value based on decimal.
                 - Truncation always reduces the value by removing decimals.
            */
        }

        static void Exercise2()
        {
            int subject1 = 76;
            int subject2 = 88;
            int subject3 = 91;

            double averageMarks = (subject1 + subject2 + subject3) / 3.0;

            int scholarshipAverage = Convert.ToInt32(averageMarks);
            Console.WriteLine(scholarshipAverage);

            /*
                Explanation:

                 - Converting double to int removes the decimal portion.
                 - Rounding may increase or decrease the value based on fractional digits.
                 - Truncation always reduces the value and can impact eligibility decisions.
            */
        }

        static void Exercise3()
        {
            decimal finePerDay = 1.75m;
            int overdueDays = 6;

            decimal totalFine = finePerDay * overdueDays;
            double analyticsFine = (double)totalFine;

            /*
                Explanation:

                The fine per day is stored as decimal because monetary values require 
                high precision and must not suffer from floating-point rounding errors.
                   
                The number of overdue days is stored as int since it is always a whole number.

                The total fine is calculated and displayed as decimal to ensure accurate
                financial representation.

                For analytics and reporting systems, the total fine is also logged as double
                because such systems prioritize performance and statistical calculations over
                exact precision.

                The conversion from decimal to double is explicit because decimal has higher
                precision than double.
            */
        }

        static void Exercise4()
        {
            decimal accountBalance = 100000m;
            float interestRateFromApi = 7.5f;

            decimal interestRate = (decimal)interestRateFromApi / 100;

            decimal monthlyInterest = accountBalance * interestRate / 12;

            accountBalance += monthlyInterest;

            /*
                Explanation:

                Implicit conversion between float and decimal may fail because
                float is a binary floating-point type and may contain values that
                cannot be represented exactly as decimal.
            */
        }

        static void Exercise5()
        {
            double cartTotal = 3499.99;
            decimal taxRate = 0.18m;
            decimal discount = 250m;

            decimal finalPayable = (decimal)cartTotal + ((decimal)cartTotal * taxRate) - discount;

            /*
                Explanation:
                
                The cart total is explicitly converted from double to decimal.
                This conversion is necessary because implicit conversion from 
                double to decimal is not allowed in C#, as it could result in 
                precision loss.
            */
        }

        static void Exercise6()
        {
            short sensorReading = 315;
            double temperatureCelsius = sensorReading / 10.0;

            int dashboardTemperature = Convert.ToInt32(temperatureCelsius);

            /*
                Explanation:
                
                The conversion from short to double is implicit and safe because
                double has a much wider range and can represent all short values
                without overflow.
                
                However, converting from double to int may cause truncation or 
                rounding effects, so controlled conversion is required.
            */
        }

        static void Exercise7()
        {
            double finalScore = 86.7;

            byte grade = Convert.ToByte(finalScore);

            /*
                Explanation:
                
                To ensure safety, the conversion process uses controlled rounding
                before casting. This avoids unintended truncation and helps keep 
                the converted value within the valid range of byte.
            */
        }

        static void Exercise8()
        {
            long usageInBytes = 8589934592;

            double usageInMB = usageInBytes / 1048576.0;

            double usageInGB = usageInBytes / 1073741824.0;

            int roundedUsage = Convert.ToInt32(usageInGB);

            /*
                Explanation:
                
                The conversion from long to double is an implicit conversion.
                This is safe because double has a wider range than long and 
                can represent all long values without overflow.

                For the monthly usage summary, the value must be shown as a 
                whole number. Converting from double to int requires an explicit 
                conversion, as decimal precision would otherwise be lost.

                Rounding is performed using Convert.ToInt32 or Math.Round, 
                which converts the value to the nearest integer.
            */
        }

        static void Exercise9()
        {
            int itemCount = 1250;
            ushort maxCapacity = 1500;


            int safeMaximumCapacity = maxCapacity;

            /*
                Explanation:
                
                When comparing or reporting these values, a conversion is required 
                because int is a signed type and ushort is an unsigned type.

                Converting ushort to int is safe because all ushort values fit 
                within the range of int.

                Converting int to ushort is risky because negative values would 
                cause overflow or unexpected results.

                Therefore, the unsigned value is converted to a signed type 
                for safe comparison.
            */
        }

        static void Exercise10()
        {
            int basicSalary = 45000;
            double allowance = 9200.50;
            double deduction = 3100.75;

            decimal netSalary = (decimal)(basicSalary + allowance - deduction);

            /*
                Explanation:

                int -> double occurs during calculation
                double -> decimal is explicitly performed

                Implicit conversion from double to decimal is not allowed in C# 
                because it may lead to hidden precision loss. Explicit conversion 
                ensures that precision handling is intentional and visible in the 
                code.
            */
        }
    }
}
