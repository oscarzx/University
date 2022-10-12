using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetAllCoursesByCategory(int categoryId);

        IEnumerable<Course> GetCoursesWithNoTemario();

        IEnumerable<Course> GetCoursesWithTemario(int temarioId);
    }
}
