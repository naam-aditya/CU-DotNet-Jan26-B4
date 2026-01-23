namespace ApplicationConfigurationTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            string appName = ApplicationConfig.ApplicationName;
            ApplicationConfig.Initialize("NotMyApp", "NotDevelopment");
            ApplicationConfig.GetConfigurationSummary();
            ApplicationConfig.ResetConfiguration();

            Console.WriteLine(ApplicationConfig.GetConfigurationSummary());
        }
    }

    class ApplicationConfig
    {
        public static string ApplicationName { get; set; }
        public static string Environment { get; set; }
        public static int AccessCount { get; set; }
        public static bool IsInitialized { get; set; }

        static ApplicationConfig()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            AccessCount = 0;
            IsInitialized = false;
            Console.WriteLine("STATIC CONSTRUCTOR EXECUTED");
        }

        public static void Initialize(string appName, string environment)
        {
            ApplicationName = appName;
            Environment = environment;
            IsInitialized = true;
            AccessCount++;
        }

        public static string GetConfigurationSummary()
        {
            AccessCount++;
            return $"Application Name: {ApplicationName}\n" + 
                $"Environment: {Environment}\n" +
                $"Access Count: {AccessCount}\n" +
                $"Initialization Status: {IsInitialized}";
        }

        public static void ResetConfiguration()
        {
            ApplicationName = "MyApp";
            Environment = "Development";
            IsInitialized = false;
            AccessCount++;
        }
    }
}