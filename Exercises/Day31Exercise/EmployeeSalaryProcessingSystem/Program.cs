using System.Globalization;

namespace EmployeeSalaryProcessingSystem;

class Program
{
    static void Main(string[] args)
    {
        CultureInfo indianCulture = new("en-IN");
        List<Employee> employees = 
        [
            new Employee { 
                Id = 1, 
                Name = "Ravi", 
                Department = "IT", 
                Salary = 80000, 
                JoinDate = new DateTime(2019,1,10) 
            },
            new Employee { 
                Id = 2, 
                Name = "Anita", 
                Department = "HR", 
                Salary = 60000, 
                JoinDate = new DateTime(2021,3,5) 
            },
            new Employee { 
                Id = 3, 
                Name = "Suresh", 
                Department = "IT", 
                Salary = 120000, 
                JoinDate = new DateTime(2018,7,15) 
            },
            new Employee { 
                Id = 4, 
                Name = "Meena", 
                Department = "Finance", 
                Salary = 90000, 
                JoinDate = new DateTime(2022,9,1) 
            }
        ];

        Console.WriteLine("HIGH AND LOW SALARY IN EACH DEPT:");
        employees.GroupBy(e => e.Department)
            .Select(
                g =>
                {
                    var min = g.Min(e => e.Salary);
                    var max = g.Max(e => e.Salary);
                    return new { Min = min, Max = max, Department = g.Key };
                }
            )
            .ToList()
            .ForEach(
                e => Console.WriteLine(
                    string.Concat(
                        $"{e.Department, -8}: ",
                        $"{e.Min.ToString("C0", indianCulture)} | ",
                        $"{e.Max.ToString("C0", indianCulture)}"
                    )
                )
            );
        
        Console.WriteLine("\nCOUNT PER DEPARTMENT:");
        employees.GroupBy(e => e.Department)
            .Select(
                g => new { Department = g.Key, Count = g.Count() }
            )
            .ToList()
            .ForEach(
                x => Console.WriteLine($"{x.Department, -8}: {x.Count}")
            );
        
        Console.WriteLine("\nEMPLOYEES JOINED AFTER 2020:");
        employees.Where(e => e.JoinDate.Year > 2020)
            .ToList()
            .ForEach(
                e => Console.WriteLine($"{e.Name, -8}: {e.JoinDate.Year}")
            );
        
        Console.WriteLine("\nEMPLOYEES NAME & SALARY:");
        employees.Select(e => new { Name = e.Name, Salary = e.Salary * 12 })
            .ToList()
            .ForEach(
                e => Console.WriteLine($"{e.Name, -8}: {e.Salary.ToString("C0", indianCulture)}")
            );
    }

    class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public double Salary { get; set; }
        public DateTime JoinDate { get; set; }

        public override string ToString() =>
            $"[Id: {Id}, Name: {Name}, Department: {Department}, Salary: {Salary}, Join Date: {JoinDate}]";
    }
}
