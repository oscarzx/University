using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentsWithCourses();
        IEnumerable<Student> GetStudentsWithNoCourses();
        IEnumerable<Student> GetStudentsWithCoursesByCourseId(int courseId);
        IEnumerable<Student> GetStudentsWithCoursesByStudentId(int studentId);
    }
}
