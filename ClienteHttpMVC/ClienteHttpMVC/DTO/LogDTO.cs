using System.ComponentModel.DataAnnotations;

namespace ClienteHttpMVC.DTO
{
    public class LogDTO
    {
        public int Id { get; set; }
        [Display(Name = "Id de gasto")]
        public int? TipoDeGastoId { get; set; }
        [Display(Name = "Tipo de gasto")]
        public string? TipoDeGasto { get; set; }
        public string? Detalle { get; set; }
        public DateTime? Fecha { get; set; }
        [Display(Name = "Id de usuario")]
        public int? UsuarioId { get; set; }
    }
}
