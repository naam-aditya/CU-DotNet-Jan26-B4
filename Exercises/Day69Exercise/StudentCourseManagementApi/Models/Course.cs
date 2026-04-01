namespace CourseManagementApi.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }

        public required ICollection<StudentCourse> StudentCourses { get; set; }
    }
}