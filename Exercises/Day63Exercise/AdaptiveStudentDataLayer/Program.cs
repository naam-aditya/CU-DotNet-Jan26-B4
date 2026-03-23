using AdaptiveStudentDataLayer.Models;
using AdaptiveStudentDataLayer.Repository;
using AdaptiveStudentDataLayer.Services;

namespace AdaptiveStudentDataLayer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(
            string.Concat(
                "Student Management System\n",
                "Create: add <storage option>\n",
                "Read: get <storage option>\n",
                "Update: up <storage option> <id>\n",
                "Remove: rm <storage option> <id>\n",
                "Terminate: q\n\n",
                "Storage Options: 1 - List | 2 - JSON\n"
            )
        );

        IStudentService studentService;

        while (true)
        {
            Console.Write("=> ");
            string? command = Console.ReadLine();
            if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command)) 
            {
                Console.WriteLine("\nINVALID COMMAND\n");
                continue;
            }

            command = command.Trim().ToLower();
            if (command == "q")
                break;

            string[] commands = command.Split(' ');

            if (commands.Length > 3)
            {
                Console.WriteLine("\nINVALID COMMAND\n");
                continue;
            }

            if (commands[0] == "get" && commands.Length == 2 && int.TryParse(commands[1], out int storage))
            { 
                if (storage == 1)
                    studentService = new StudentService(new ListStudentRepository());
                else if (storage == 2)
                    studentService = new StudentService(new JsonStudentRepository());
                else
                {
                    Console.WriteLine("\nINVALID STORAGE OPTION\n");
                    continue;
                }

                Console.WriteLine(
                    string.Concat(
                        '\n',
                        string.Join(
                            '\n',
                            studentService.GetStudents()
                        ),
                        '\n'
                    )
                );
            }

            if (commands[0] == "add" && commands.Length == 2)
            {
                string? name;
                while (true)
                {
                    Console.Write("Name: ");
                    name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("\nENTER VALID NAME\n");
                        continue;
                    }

                    break;
                }

                string? gradeInput;
                while (true)
                {
                    Console.Write("Grade: ");
                    gradeInput = Console.ReadLine();
                    if (
                        string.IsNullOrEmpty(gradeInput) || 
                        string.IsNullOrWhiteSpace(gradeInput) ||
                        !int.TryParse(gradeInput, out int grade)
                    )
                        Console.WriteLine("\nENTER VALID GRADE\n");
                    else
                        break;
                }

                if (commands[1] == "1")
                    studentService = new StudentService(new ListStudentRepository());
                else if (commands[1] == "2")
                    studentService = new StudentService(new JsonStudentRepository());
                else
                {
                    Console.WriteLine("\nINVALID COMMAND\n");
                    continue;
                }

                studentService.AddStudent(
                    new Student(name, int.Parse(gradeInput))
                );
            }
        }
    }
}
