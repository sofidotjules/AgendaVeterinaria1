using System.ComponentModel.DataAnnotations;

namespace AgendaVeterinaria1.Models
{
    public class Especialidad
    {

        [Key]
        public int IDEspecialidad { get; set; }
        public string? Descripcion { get; set; }
        public List<Profesional> Profesionales { get; set; }

    }
}
