using System.ComponentModel.DataAnnotations;

namespace ClienteHttpMVC.DTO
{
    public class ListadoUsuariosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Contrasena { get; set; }
        [Display(Name = "Nombre de equipo")]
        public string NombreEquipo { get; set; }
    }
}
