using System.ComponentModel.DataAnnotations;

namespace ClienteHttpMVC.DTO
{
    public class PagosUsuarioDTO
    {
        public int Id { get; set; }

        [Display(Name = "Metodo de pago")]
        public string MetodoDePago { get; set; }

        [Display(Name = "Tipo de gasto")]
        public string TipoDeGasto { get; set; }
        public double Monto { get; set; }

        [Display(Name = "Tipo de pago")]
        public string TipoDePago { get; set; }

        public string Descripcion { get; set; }
    }
    
}
