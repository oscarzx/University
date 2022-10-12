using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CoursesService : ICoursesService
    {
        public IEnumerable<Course> GetAllCoursesByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesWithNoTemario()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetCoursesWithTemario(int temarioId)
        {
            throw new NotImplementedException();
        }
    }
}
