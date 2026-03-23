using System.Text.Json;
using AdaptiveStudentDataLayer.Models;

namespace AdaptiveStudentDataLayer.Repository;

public class JsonStudentRepository : IStudentRepository
{
    private readonly string fileName = "students.json";
    private static List<Student> _students = [];
    public void AddStudent(Student student)
    {
        using StreamWriter streamWriter = new(fileName);
        _students.Add(student);
        streamWriter.WriteLine(JsonSerializer.Serialize(_students));
    }

    public IEnumerable<Student> GetStudents()
    {
        List<Student>? students = [];
        using StreamReader streamReader = new(fileName);

        string? json = streamReader.ReadToEnd();
        if (json != null)
            students = JsonSerializer.Deserialize<List<Student>>(json);
        
        if (students != null)
            return students;

        return [];
    }

    public void RemoveStudent(Student student)
    {
        IEnumerable<Student> students = GetStudents();
        var data = students.FirstOrDefault(s => s.Id == student.Id);
        if (data != null)
            students.ToList().Remove(data);
        
        using StreamWriter streamWriter = new(fileName);
        streamWriter.WriteLine(JsonSerializer.Serialize(students));
    }

    public void UpdateStudent(Student student)
    {
        _students = GetStudents().ToList();
        var data = _students.FirstOrDefault(s => s.Id == student.Id);
        if (data != null)
        {
            data.Name = student.Name;
            data.Grade = student.Grade;
        }

        using StreamWriter streamWriter = new(fileName);
        streamWriter.WriteLine(JsonSerializer.Serialize(_students));
    }

    public void RemoveStudentById(int id)
    {
        _students = GetStudents().ToList();
        var data = _students.Find(s => s.Id == id);
        if (data != null)
            _students.Remove(data);
        
        using StreamWriter streamWriter = new(fileName);
        streamWriter.WriteLine(JsonSerializer.Serialize(_students));
    }
}
