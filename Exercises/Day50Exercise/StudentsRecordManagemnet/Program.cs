namespace StudentsRecordManagemnet;

class Program
{
    static void Main(string[] args)
    {
        StudentRecords.AddStudentRecord(new() { Id=1, Name="Ramesh" }, 46);
        StudentRecords.AddStudentRecord(new() { Id=2, Name="Suresh" }, 27);
        StudentRecords.AddStudentRecord(new() { Id=3, Name="Kamlesh" }, 87);
        StudentRecords.AddStudentRecord(new() { Id=4, Name="Mukesh" }, 27);
        StudentRecords.AddStudentRecord(new() { Id=5, Name="Dharmesh" }, 35);
        StudentRecords.AddStudentRecord(new() { Id=6, Name="Rajesh" }, 88);
        StudentRecords.AddStudentRecord(new() { Id=1, Name="Ramesh" }, 89);

        Console.WriteLine(StudentRecords.GetAllRecords());
    }
}

public class Student
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public override bool Equals(object? obj) =>
        (obj is Student other) && Id == other.Id && Name == other.Name;

    public override int GetHashCode() =>
        HashCode.Combine(Id, Name);
}

public class StudentRecords
{
    public static Dictionary<Student, int> _studentRecords = [];

    public static void AddStudentRecord(Student student, int marks)
    {
        if (!_studentRecords.ContainsKey(student))
            _studentRecords[student] = marks;
        
        else if (_studentRecords[student] < marks)
            _studentRecords[student] = marks;
    }

    public static string GetAllRecords() =>
        string.Join(
            '\n',
            _studentRecords.Select(r => $"({r.Key.Id}, {r.Key.Name}): {r.Value}")
        );
}
