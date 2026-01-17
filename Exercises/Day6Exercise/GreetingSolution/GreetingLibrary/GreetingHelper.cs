namespace GreetingLibrary;

public class GreetingHelper
{
    public static string GetGreeting(string? name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
        {
            name = "Guest";
        }

        return $"Hello, {name}!";
    }
}
