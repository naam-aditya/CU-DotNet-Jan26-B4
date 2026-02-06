using System.Text;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            HangMan();
        }
        static void HangMan()
        {
            string[] words = [
                "Freedom",
                "Laptop",
                "Jungle",
                "Sunlight",
                "Election",
                "Smartphone"
            ];

            bool[] guessedLetters = new bool[26];

            Random random = new();
            int lives = 6;

            int randomWordIndex = random.Next(0, words.Length);
            char[] word = new char[words[randomWordIndex].Length];

            StringBuilder stringBuilder = new(',');

            for (int i = 0; i < word.Length; i++)
                word[i] = '_';

            Console.WriteLine("Welcome to C# Hangman!\n");

            while (true)
            {
                if (lives == 0)
                {

                    Console.WriteLine("You lost!");
                    break;
                }

                if (!word.Contains('_'))
                {
                    Console.Write("Word: ");
                    for (int i = 0; i < word.Length; i++)
                        Console.Write($"{word[i]} ");
                    Console.WriteLine("\nYou won!");
                    break;
                }

                Console.Write("Word: ");
                for (int i = 0; i < word.Length; i++)
                    Console.Write($"{word[i]} ");

                Console.WriteLine($"\nLives: {lives}");
                if (stringBuilder.ToString().Length != 0)
                    Console.Write($"Guessed: {stringBuilder.ToString()[2..]}");
                else
                    Console.Write($"Guessed: ");

                Console.Write("\nGuess a letter: ");
                string? input = Console.ReadLine();
                if (
                    string.IsNullOrEmpty(input) || 
                    string.IsNullOrWhiteSpace(input) ||
                    input.Length > 1 ||
                    !char.TryParse(input, out char letter) ||
                    !char.IsBetween(char.ToUpper(letter), 'A', 'Z')
                )
                {
                    Console.WriteLine("Please enter a valid letter.\n");
                    continue;
                }

                letter = char.ToUpper(letter);
                if (!words[randomWordIndex].ToUpper().Contains(letter))
                {
                    Console.WriteLine("Nope, try again.\n");
                    lives--;
                    continue;
                }
                else if (guessedLetters[letter - 'A'])
                {
                    Console.WriteLine($"You already guessed {letter}. Try Again.\n");
                    lives--;
                    continue;
                }
                
                Console.WriteLine("Good catch!\n");
                guessedLetters[letter - 'A'] = true;
                stringBuilder.Append($", {letter}");

                for (int i = 0; i < words[randomWordIndex].Length; i++)
                    if (words[randomWordIndex].ToUpper()[i] == letter)
                        word[i] = letter;
            }
        }
    }
}