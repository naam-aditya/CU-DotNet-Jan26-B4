using CourseManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagementApi.Data;

namespace StudentCourseManagementApi.Controllers
{
    [ApiController]
    [Route("api/enroll")]
    public class EnrollmentController : Controller
    {
        private readonly AppDbContext _context;
        public EnrollmentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            var exists = await _context.StudentCourses
                            .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            
            if (exists)
                return BadRequest("Already enrolled");
            
            var enrollment = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _context.StudentCourses.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok("Enrollment successful");
        }
    }
}