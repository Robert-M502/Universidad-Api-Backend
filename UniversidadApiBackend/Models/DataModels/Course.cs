using System.ComponentModel.DataAnnotations;

namespace UniversidadApiBackend.Models.DataModels
{
    public enum Lavel { 
        Basic,
        Medium,
        Advance,
        Expert
    }
    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public Lavel Lavel { get; set; } = Lavel.Basic;
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>(); // Relación hacia las categorias
        [Required]
        public Chapter Chapter { get; set; } = new Chapter(); // Relación hacia los Chapter->Temas
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>(); // Relación hacia los estudiantes
    }
}
