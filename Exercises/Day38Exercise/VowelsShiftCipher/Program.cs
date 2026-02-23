using System.Text;
namespace VowelsShiftCipher;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(VowelShiftCipher("hello"));
    }

    static string VowelShiftCipher(string input)
    {
        string vowels = "aeiou";
        StringBuilder stringBuilder = new();

        foreach (var letter in input)
        {
            if (vowels.Contains(letter))
                switch (letter)
                {
                    case 'a':
                        stringBuilder.Append('e');
                        break;
                    case 'e':
                        stringBuilder.Append('i');
                        break;
                    case 'i':
                        stringBuilder.Append('o');
                        break;
                    case 'o':
                        stringBuilder.Append('u');
                        break;
                    case 'u':
                        stringBuilder.Append('a');
                        break;
                    default:
                        break;
                }
            
            else if (letter == 'z')
                stringBuilder.Append('b');
            else
            {   
                char nextChar;
                int temp = 1;
                do
                {
                    int next = letter + temp++;
                    nextChar = (char) next;
                }
                while (vowels.Contains(nextChar));

                stringBuilder.Append(nextChar);
            }
        }

        return stringBuilder.ToString();
    }
}
