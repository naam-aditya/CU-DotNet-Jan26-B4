using AdaptiveStudentDataLayer.Models;

namespace AdaptiveStudentDataLayer.Repository;

public interface IStudentRepository
{
    void AddStudent(Student student);
    void UpdateStudent(Student student);
    void RemoveStudent(Student student);
    IEnumerable<Student> GetStudents();
}