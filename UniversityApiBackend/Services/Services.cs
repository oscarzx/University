using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class Services
    {
        private readonly UniversityDbContext _context;

        public Services(UniversityDbContext context)
        {
            _context = context;
        }

        // Buscar usuarios por email
        public IEnumerable<User> GetUserByEmail(string email)
        {
            var userList = _context.Users?.ToList();

            var userMail = from user in userList
                           where user.Mail.Contains(email)
                           select user;
            return userMail;
        }

        // Buscar alumnos mayores de edad
        public void GetStudentAdult()
        {
            var studentList = _context.Students?.ToList();

            //var studentAdult = studentList.Where(s => )
        }


        //Buscar alumnos que tengan al menos un curso
        public void GetStudentWithSomeCourse()
        {
            var studentList = _context.Students?.ToList();

            var studentListWhitCourse = studentList.Any(c => c.Courses.Any());
        }

        //Buscar cursos de un nivel determinado que al menos tengan un alumno inscrito
        public void GetCourseWhitStudent(Level level)
        {
            var courseList = _context.Courses?.ToList();

            if (courseList != null)
            {
                var courseWithStudents = courseList.Where(c => c.Students.Any() && c.Level == level);
            }
        }

        //Buscar cursos de un nivel determinado que sean de una categoría determinada
        public void GetCourseWithCategy(Level level)
        {
            var courseList = _context.Courses?.ToList();

            if (courseList != null)
            {
                var courseWithStudents = courseList.Where(c => c.Categories.Any() && c.Level == level);
            }
        }


        //Buscar cursos sin alumnos
        public void GetCoursesWithoutStudents()
        {
            var list = _context.Courses?.ToList();

            var courseWithoutStudents = from course in list
                                        where course.Students == null
                                        select course;
        }
    }
}
