using AdaptiveStudentDataLayer.Models;

namespace AdaptiveStudentDataLayer.Services;

public interface IStudentService
{
    void AddStudent(Student student);
    IEnumerable<Student> GetStudents();
    void RemoveStudentById(int id);
    void UpdateStudent(Student student);
}