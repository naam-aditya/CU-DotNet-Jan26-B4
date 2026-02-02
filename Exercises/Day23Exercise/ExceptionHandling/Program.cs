namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            DivideNums(3, 0);
            ConvertToInt("p");
            GetElement([3, 5, 6], 5);
            GetStudentAge();
            GetStudentName();
        }

        static int DivideNums(int x, int y)
        {
            try
            {
                return x / y;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division by zero is not possible.");
            }
            finally
            {
                Console.WriteLine("Operation Completed\n");
            }
            return 0;
        }

        static int ConvertToInt(string data)
        {
            try
            {
                return Convert.ToInt32(data);
            }
            catch (FormatException)
            {
                Console.WriteLine("Incorrect format");
            }
            finally
            {
                Console.WriteLine("Operation Completed\n");
            }
            return 0;
        }

        static int GetElement(int[] arr, int index)
        {
            try
            {
                return arr[index];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Index is out of range");
            }
            finally
            {
                Console.WriteLine("Operation Completed\n");
            }
            return 0;
        }

        static int GetStudentAge()
        {
            int studentAge = 0;
            while (true)
            {
                try
                {
                    Console.Write("Enter Student Age (18-60): ");
                    studentAge = Convert.ToInt32(Console.ReadLine());

                    if (studentAge < 18 || studentAge > 60)
                        throw new InvalidStudentAgeException("Student Age must be between 18 and 60.");

                    Console.WriteLine("Student Age is valid.\n");
                    break;
                }
                catch (InvalidStudentAgeException ex)
                {
                    Console.WriteLine($"Custom Error: {ex.Message}\n");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Please enter age as an integer.\n");
                }
            }
            return 0;
        }

        static string GetStudentName()
        {
            string? studentName = "";

            while (true)
            {
                try
                {
                    Console.Write("Enter Student Name: ");
                    studentName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(studentName) || studentName.Length < 3)
                        throw new InvalidStudentNameException("Student name must be at least 3 characters and not empty.");

                    Console.WriteLine("Student Name is valid.\n");
                    break;
                }
                catch (InvalidStudentNameException ex)
                {
                    Console.WriteLine($"Custom Error: {ex.Message}\n");
                }
            }

            return studentName;
        }
    }

    class InvalidStudentAgeException(string message) : Exception(message) { }
    class InvalidStudentNameException(string message) : Exception(message) { }
}