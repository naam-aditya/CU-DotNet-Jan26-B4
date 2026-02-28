namespace BonusCalculationSystem.Test;

public class BonusCalculationSystemTests
{
    private EmployeeBonus? _bonus;

    [SetUp]
    public void Setup()
    {
        _bonus = null;
    }

    [TestCase(500_000, 5, 6, 1.1, 95, 123_200)]
    [TestCase(400_000, 4, 8, 1.0, 80, 60_480)]
    [TestCase(1_000_000, 5, 15, 1.5, 95, 280_000)]
    [TestCase(0, 5, 15, 1.5, 95, 0)]
    [TestCase(300_000, 2, 3, 1.0, 90, 13_500)]
    [TestCase(600_000, 3, 0, 1.0, 100, 64_800)]
    [TestCase(900_000, 5, 11, 1.2, 100, 226_800)]
    [TestCase(555_555, 4, 6, 1.13, 92, 118_649.88)]
    public void Test1(
        decimal baseSalary, int performanceRating, 
        int yearsOfExperience, decimal departmentMultiplier, 
        double attendancePercentage, decimal expected
    )
    {
        _bonus = new()
        {
            BaseSalary = baseSalary,
            PerformanceRating = performanceRating,
            YearsOfExperience = yearsOfExperience,
            DepartmentMultiplier = departmentMultiplier,
            AttendancePercentage = attendancePercentage
        };
        var result = _bonus.CalculateNetAnnualBonus();
        Assert.That(result, Is.EqualTo(expected));
    }
}