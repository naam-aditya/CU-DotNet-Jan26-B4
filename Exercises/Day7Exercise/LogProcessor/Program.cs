using System.Security.AccessControl;

namespace LogProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                InvalidAccessLog();
                return;
            }

            string[] inputs = input.Split("|");

            if (inputs.Length != 5)
            {
                InvalidAccessLog();
                return;
            }

            if (
                inputs[0].Length != 2 ||
                !char.IsLetter(inputs[0][0]) ||
                !char.IsUpper(inputs[0][0]) ||
                !char.IsDigit(inputs[0][1])
            )
            {
                InvalidAccessLog();
                return;
            }

            if (
                inputs[1].Length != 1 ||
                !char.IsLetter(Convert.ToChar(inputs[1])) ||
                !char.IsUpper(Convert.ToChar(inputs[1]))
            )
            {
                InvalidAccessLog();
                return;
            }

            if (
                inputs[2].Length != 1 ||
                !char.IsDigit(Convert.ToChar(inputs[2])) ||
                !char.IsBetween(Convert.ToChar(inputs[2]), '1', '7')
            )
            {
                InvalidAccessLog();
                return;
            }

            if (
                inputs[3] != "true" &&
                inputs[3] != "false"
            )
            {
                InvalidAccessLog();
                return;
            }

            if (inputs[4].Length <= 3)
            {
                for (int i = 0; i < inputs[4].Length; i++)
                {
                    if (!char.IsDigit(inputs[4][i]))
                    {
                        InvalidAccessLog();
                        return;
                    }
                }
            }
            
            string gateCode = inputs[0];
            char userInitial = Convert.ToChar(inputs[1]);
            byte accessLevel = Convert.ToByte(inputs[2]);
            bool isActive = Convert.ToBoolean(inputs[3]);
            byte attempts = Convert.ToByte(inputs[4]);

            // buisness logic

            string status;

            if (!isActive)
            {
                status = "ACCESS DENIED - INACTIVE USER";
            }
            else if (attempts > 100)
            {
                status = "ACCESS DENIED - TOO MANBY ATTEMPTS";
            }
            else if (accessLevel >= 5)
            {
                status = "ACCESS GRANTED - HIGH SECURITY";
            }
            else
            {
                status = "ACCESS GRANTED - STANDARD";
            }

            Console.WriteLine($"{"Gate".PadRight(7)}: {gateCode}");
            Console.WriteLine($"{"User".PadRight(7)}: {userInitial}");
            Console.WriteLine($"{"Level".PadRight(7)}: {accessLevel}");
            Console.WriteLine($"{"Attempts".PadRight(9)}: {attempts}");
            Console.WriteLine($"{"Status".PadRight(8)}: {status}");
        }

        static void InvalidAccessLog()
        {
            Console.WriteLine("INVALID ACCESS LOG");
        }
    }
}