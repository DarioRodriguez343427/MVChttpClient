using System.ComponentModel.DataAnnotations;

namespace ClienteHttpMVC.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe especificar un correo")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Debe especificar una contraseña")]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public string token { get; set; }
        public int Equipo { get; set; }
    }
}
