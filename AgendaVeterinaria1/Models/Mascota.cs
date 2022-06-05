using System.ComponentModel.DataAnnotations;

namespace AgendaVeterinaria1.Models
{
    public class Mascota
    {
        [Key]
        public int IDMascota { get; set; }
        public string Nombre { get; set; }
        public string TipoMascota { get; set; }
        public string Detalle { get; set; }

    }
}
