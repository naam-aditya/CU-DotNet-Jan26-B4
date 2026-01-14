using GreetingLibrary;

namespace GreetingApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string? name = Console.ReadLine();
            Console.WriteLine(GreetingHelper.GetGreeting(name));
        }
    }
}