namespace BonusCalculationSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();
    }
}

public class EmployeeBonus
{
    private int performanceRating;
    private double attendancePercentage;
    public decimal BaseSalary { get; set; }
    public int PerformanceRating
    {
        get => performanceRating;
        set 
        {
            if (value < 1 || value > 5)
                throw new InvalidOperationException("Rating must me between 1 - 5.");
            performanceRating = value;
        }
    }
    
    public int YearsOfExperience { get; set; }
    public decimal DepartmentMultiplier { get; set; }
    public double AttendancePercentage
    {
        get => attendancePercentage;
        set 
        {
            if (value < 0 || value > 100)
                throw new InvalidOperationException("Attendance must be between 0 - 100."); 
            attendancePercentage = value; 
        }
    }
    
    public decimal NetAnnualBonus { get; private set; }

    public decimal CalculateNetAnnualBonus()
    {
        if (BaseSalary <= 0)
            return 0;
        
        // Step 1: Base Bonus Percentage

        if (PerformanceRating == 5)
            NetAnnualBonus += BaseSalary * 0.25m;
        else if (PerformanceRating == 4)
            NetAnnualBonus += BaseSalary * 0.18m;
        else if (PerformanceRating == 3)
            NetAnnualBonus += BaseSalary * 0.12m;
        else if (PerformanceRating == 2)
            NetAnnualBonus += BaseSalary * 0.05m;
        else
            NetAnnualBonus = 0;

        // Step 2: Experience Bonus

        if (YearsOfExperience > 10)
            NetAnnualBonus += BaseSalary * 0.05m;
        else if (YearsOfExperience > 5)
            NetAnnualBonus += BaseSalary * 0.03m;

        // Step 3: Attendance Penalty
        
        if (AttendancePercentage < 85)
            NetAnnualBonus -= NetAnnualBonus * 0.20m;
        
        // Step 4: Department Multiplier
        
        NetAnnualBonus *= DepartmentMultiplier;

        // Step 5: Maximum Cap

        if (NetAnnualBonus > BaseSalary * 0.40m)
            NetAnnualBonus = BaseSalary * 0.40m;

        // Step 6: Tax Deduction
        
        if (NetAnnualBonus > 300_000)
            NetAnnualBonus -= NetAnnualBonus * 0.30m;
        else if (NetAnnualBonus > 150_000 && NetAnnualBonus <= 300_000)
            NetAnnualBonus -= NetAnnualBonus * 0.20m;
        else if (NetAnnualBonus <= 150_000)
            NetAnnualBonus -= NetAnnualBonus * 0.10m;

        return Math.Round(NetAnnualBonus, 2);
    }
}
