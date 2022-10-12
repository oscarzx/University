using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetAllCourseByCategory(int categoryId);
    }
}
