using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaVeterinaria1.Models
{
    public class Agenda
    {
        [Key]
        public int IDAgenda { get; set; }
        public string? FranjaHoraria { get; set; }
        public int TopeDeTurnos { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }

        [ForeignKey("IDProfesional")]
        public Profesional Profesional { get; set; }
        public int IDProfesional { get; set; }

    }
}
