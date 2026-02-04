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
                if (!char.IsBetween(pin.KeyChar, '0', '9'))
                    continue;
                
                Console.Write("*");
                pincode.Append(pin.KeyChar);
            }
            while (pincode.Length != 4);

            Console.WriteLine($"\nPIN: {pincode}");
        }
    }
}