namespace AdaptiveStudentDataLayer.Models;

public class Student
{
    private static int _id = 1;

    public Student() { }

    public Student(string name, int grade)
    {
        Id = _id++;
        Name = name;
        Grade = grade;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Grade { get; set; }

    public override string ToString() =>
        $"Id: {Id}, Name: {Name}, Grade: {Grade}";
}