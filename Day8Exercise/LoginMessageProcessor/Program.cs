namespace LoginMessageProcessor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            string status = string.Empty;

            string[] inputs = input.Split('|', StringSplitOptions.TrimEntries);
            if (inputs.Length != 2)
            {
                status = LoginFailed();
            }

            string loginMessage = inputs[1].ToLower();
            string userName = inputs[0];

            if (!loginMessage.Contains("successful"))
            {
                status = LoginFailed();
            }
            else if (loginMessage.Equals("login successful"))
            {
                status = LoginSuccess();
            }
            else
            {
                status = LoginSuccess() + " (CUSTOM MESSAGE)";
            }

            Console.WriteLine($"{"User", -9}: {userName}");
            Console.WriteLine($"{"Message", -9}: {loginMessage}");
            Console.WriteLine($"{"Status", -9}: {status}");
        }

        static string LoginFailed()
        {
            return "LOGIN FAILED";
        }

        static string LoginSuccess()
        {
            return "LOGIN SUCCESS";
        }
    }
}