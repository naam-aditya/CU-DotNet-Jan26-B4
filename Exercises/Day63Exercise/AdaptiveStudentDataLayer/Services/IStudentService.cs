using AdaptiveStudentDataLayer.Models;

namespace AdaptiveStudentDataLayer.Services;

public interface IStudentService
{
    void AddStudent(Student student);
    IEnumerable<Student> GetStudents();
}