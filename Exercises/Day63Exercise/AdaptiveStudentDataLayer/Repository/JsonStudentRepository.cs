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
        throw new NotImplementedException();
    }

    public void UpdateStudent(Student student)
    {
        throw new NotImplementedException();
    }
}
