namespace EmployeeCompensationManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee employee01 = new PermanentEmployee(101, "Aditya", 10000, 5);
            PermanentEmployee employee02 = new(101, "Aditya", 10000, 5);

            Console.WriteLine($"Base Employee: {employee01.CalculateAnnualSalary():N2}");
            Console.WriteLine($"Permanent Employee: {employee02.CalculateAnnualSalary():N2}");
        }

        class Employee
        {
            public int EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public decimal BasicSalary { get; set; }
            public int ExperienceInYears { get; set; }

            public Employee(int employeeId, string employeeName, decimal basicSalary, int experienceInYears)
            {
                EmployeeId = employeeId;
                EmployeeName = employeeName;
                BasicSalary = basicSalary;
                ExperienceInYears = experienceInYears;
            }

            public Employee()
            {
                EmployeeId = 0;
                EmployeeName = string.Empty;
                BasicSalary = 0;
                ExperienceInYears = 0;
            }

            public decimal CalculateAnnualSalary()
            {
                return BasicSalary * 12;
            }

            public void DisplayEmployeeDetails()
            {
                Console.WriteLine(
                    $"Id: {EmployeeId}, Name: {EmployeeName}, Salary: {BasicSalary}, Experience: {ExperienceInYears}"
                );
            }
        }

        class PermanentEmployee(
            int employeeId,
            string employeeName,
            decimal basicSalary,
            int experienceInYears
        ) : Employee(employeeId, employeeName, basicSalary, experienceInYears)
        {
            public new decimal CalculateAnnualSalary()
            {
                decimal houseRentAllowance = BasicSalary * 0.2m;
                decimal specialAllowance = BasicSalary * 0.1m;
                decimal loyaltyBonus = (ExperienceInYears >= 5) ? 50000 : 0;

                return base.CalculateAnnualSalary() + houseRentAllowance + specialAllowance + loyaltyBonus;
            }
        }

        class ContractEmployee(
            int employeeId, 
            string employeeName, 
            decimal basicSalary, 
            int experienceInYears, 
            int contractDurationInMonths
        ) : Employee(employeeId, employeeName, basicSalary, experienceInYears)
        {
            public int ContractDurationInMonths { get; set; } = contractDurationInMonths;

            public new decimal CalculateAnnualSalary()
            {
                decimal completionBonus = (ContractDurationInMonths >= 12) ? 30000 : 0;
                return base.CalculateAnnualSalary() + completionBonus;
            }
        }

        class InternEmployee(
            int employeeId,
            string employeeName,
            decimal basicSalary,
            int experienceInYears
        ) : Employee(employeeId, employeeName, basicSalary, experienceInYears)
        {
            public new decimal CalculateAnnualSalary()
            {
                return base.CalculateAnnualSalary();
            }
        }
    }
}