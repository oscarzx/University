using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Student : BaseEntity
    {
        [Required]
        public string FirtsName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime DoB { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
