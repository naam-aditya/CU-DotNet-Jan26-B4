namespace LineMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrintPattern();
            PrintPattern('+');
            PrintPattern('$', 60);
        }

        static void PrintPattern(char ch = '-', int num = 40)
        {
            for (int i = 0; i < num; i++)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
        }
    }
}