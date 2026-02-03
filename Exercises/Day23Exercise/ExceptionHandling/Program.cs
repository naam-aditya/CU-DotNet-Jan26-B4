namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            DivideNums(3, 0);
            ConvertToInt("p");
            GetElement([3, 5, 6], 5);
            ValidateStudentData();
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
                    Exception wrappedException = new("Student AgeValidation Failed!", ex);
                    Console.WriteLine($"Exception: {wrappedException.Message}\n");
                    Console.WriteLine($"Custom Error: {wrappedException.InnerException!.Message}\n");

                    throw wrappedException;
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
                        throw new InvalidStudentNameException("Invalid Name: Student name must be at least 3 characters and not empty.");


                    foreach (var ch in studentName)
                        if (!char.IsLetter(ch))
                            throw new InvalidStudentNameException("Invalid Name: Name must contain letters only");
                    
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

        static void ValidateStudentData()
        {
            Console.WriteLine("\n--- Student Validation (Custom Exceptions) ---");

            try
            {
                string name = GetStudentName();
                int age = GetStudentAge();

                Console.WriteLine($"\nStudent Registered Successfully!");
                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Age : {age}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n=== Exception Log ===");
                Console.WriteLine("Message:");
                Console.WriteLine(ex.Message);

                Console.WriteLine("\nStackTrace:");
                Console.WriteLine(ex.StackTrace);

                Console.WriteLine("\nInnerException:");
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : "No Inner Exception");
            }
            finally
            {
                Console.WriteLine("Operation Completed");
            }
        }
    }

    class InvalidStudentAgeException(string message) : Exception(message) { }
    class InvalidStudentNameException(string message) : Exception(message) { }
}