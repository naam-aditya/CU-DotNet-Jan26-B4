namespace EmployeeClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new();

            employee.SetId(1);
            employee.Name = "Aditya Bhardwaj";
            employee.Department = Departments.IT;
            employee.Salary = 80000;

            employee.Display();
        }
    }

    internal enum Departments
    {
        Accounts,
        Sales,
        IT
    }

    internal class Employee
    {
        int id;

        public string? Name { get; set; }

        private Departments department;
        public Departments Department
        {
            get { return department; }
            set { department = value; }
        }

        private int salary;
        public int Salary
        {
            get { return salary; }
            set
            {
                if (value >= 50000 || value <= 90000)
                {
                    salary = value;
                }
            }
        }

        public int GetId() { return id; }

        public void SetId(int id) { this.id = id; }

        public void Display()
        {
            Console.WriteLine(
                $"{"ID",-11}: {id}\n" +
                $"{"NAME",-11}: {Name}\n" +
                $"{"DEPARTMENT",-11}: {Department}\n" +
                $"{"SALARY",-11}: {Salary}"
            );
        }
    }
}