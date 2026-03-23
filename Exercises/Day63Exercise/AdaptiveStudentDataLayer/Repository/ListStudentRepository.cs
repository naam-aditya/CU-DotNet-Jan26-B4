using AdaptiveStudentDataLayer.Models;

namespace AdaptiveStudentDataLayer.Repository;

public class ListStudentRepository : IStudentRepository
{
    private static List<Student> _students = [];

    public void AddStudent(Student student) =>
        _students.Add(student);

    public IEnumerable<Student> GetStudents() =>
        _students;

    public void RemoveStudent(Student student) =>
        _students.Remove(student);
    
    public void RemoveStudentById(int id)
    {
        var student = _students.Find(s => s.Id == id);
        if (student != null)
            _students.Remove(student);
    }

    public void UpdateStudent(Student student)
    {
        var data = _students.Find(s => s.Id == student.Id);
        if (data != null)
        {
            data.Name = student.Name;
            data.Grade = student.Grade;
            return;
        }

        throw new NullReferenceException();
    }
}