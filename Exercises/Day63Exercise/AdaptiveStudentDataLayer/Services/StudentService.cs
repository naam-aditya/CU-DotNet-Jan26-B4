using AdaptiveStudentDataLayer.Models;
using AdaptiveStudentDataLayer.Repository;

namespace AdaptiveStudentDataLayer.Services;

public class StudentService : IStudentService
{
    private IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public void AddStudent(Student student)
    {
        if (string.IsNullOrEmpty(student.Name) || string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentException("Enter valid Name.");
        
        if (student.Grade < 0 || student.Grade > 100)
            throw new ArgumentException("Grade must be between 0 - 100");
        
        _studentRepository.AddStudent(student);
    }

    public IEnumerable<Student> GetStudents() =>
        _studentRepository.GetStudents();
}