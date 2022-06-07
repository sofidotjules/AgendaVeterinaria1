using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaVeterinaria1.Models
{
    public class Profesional
    {
        [Key]
        public int IDProfesional { get; set; }

        [ForeignKey("IDUsuario")]
        public Usuario Usuario { get; set; }
        public int IDUsuario { get; set; }

        public string? Matricula { get; set; }

        public string? Nombre { get; set; }

        public int DNI { get; set; }

        public string TipoProfesional  { get; set; }

        public string? Email { get; set; }
    }
}
