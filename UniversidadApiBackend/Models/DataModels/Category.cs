using System.ComponentModel.DataAnnotations;

namespace UniversidadApiBackend.Models.DataModels
{
    public class Category: BaseEntity
    {
        [Required]
        public string Neme { get; set; } = string.Empty;
        public ICollection<Course> Courses { get; set;} = new List<Course>(); // Relación hacia los cursos
    }
}
