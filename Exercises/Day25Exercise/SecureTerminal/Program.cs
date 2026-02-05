using System.Text;

namespace SecureTerminal
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder pincode = new();
            do
            {
                var pin = Console.ReadKey(true);
                if (pin.Key == ConsoleKey.Backspace && pincode.Length >= 1)
                {
                    Console.Write("\b \b");
                    pincode.Remove(pincode.Length - 1, 1);
                }

                if (pincode.Length == 4 && pin.Key == ConsoleKey.Enter)
                    break;

                if (!char.IsBetween(pin.KeyChar, '0', '9') || pincode.Length == 4)
                    continue;
                
                Console.Write("*");
                pincode.Append(pin.KeyChar);

            }
            while (true);

            Console.WriteLine($"\nPIN: {pincode}");
        }
    }
}