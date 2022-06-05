using System.ComponentModel.DataAnnotations;

namespace AgendaVeterinaria1.Models
{
    public class Turno
    {[Key]
        public int IDTurno { get; set; }
        public string TipoDeTurno { get; set; }
        public string  Detalle { get; set; }

        public DateTime Fecha { get; set; }

        public string Horario { get; set; }
        public int IDCliente { get; set; }
        public Cliente Cliente { get; set; }
        public int IDProfesional { get; set; }
        public Profesional Profesional { get; set; }

    }
}
