namespace DailyLogger
{
    class Program
    {
        static void Main(string[] args)
        {

            string fileName = "journal.txt";
            string directoryName = "DailyLogger/";
            
            string? data = string.Empty;
            using StreamWriter streamWriter = new($"{directoryName}{fileName}", true);

            do
            {
                Console.Write("> ");
                data = Console.ReadLine();

                if (string.IsNullOrEmpty(data) || string.IsNullOrWhiteSpace(data))
                    break;
                
                streamWriter.WriteLine(data);
            }
            while (true);
        }
    }
}