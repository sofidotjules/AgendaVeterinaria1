using System.ComponentModel.DataAnnotations;

namespace AgendaVeterinaria1.Models
{
    public class Usuario
    {[Key]
        public int IDUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string TipoDeUsuario { get; set; }
    }
}
