using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaVeterinaria1.Models
{
    public class Cliente
    {
        [Key]
        public int IDCliente { get; set; }
        public string Nombre { get; set; }
        public int DNI { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public List<Mascota> Mascotas { get; set; }

    }
}
