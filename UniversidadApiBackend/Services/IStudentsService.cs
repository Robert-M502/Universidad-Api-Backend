using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Services
{
    public interface IStudentsService
    {
        IEnumerable<Student> GetStudentWithCourses();
        IEnumerable<Student> GetStudentWithNoCourses();

    }
}
