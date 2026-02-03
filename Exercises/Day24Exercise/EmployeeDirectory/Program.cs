using System.Collections;

namespace EmployeeDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable hashtable = [];
            hashtable.Add(101, "Alice");
            hashtable.Add(102, "Bob");
            hashtable.Add(103, "Charlie");
            hashtable.Add(104, "Diana");

            if (hashtable[105] != null)
                Console.WriteLine("ID already exists.\n");
            else
                hashtable.Add(105, "Edward");
            
            string? name = (string?) hashtable[102];

            foreach (DictionaryEntry item in hashtable)
                Console.WriteLine($"{item.Key}: {item.Value}");
            
            hashtable.Remove(103);
            Console.WriteLine($"\nTotal count after deletion: {hashtable.Count}.");
        }
    }
}