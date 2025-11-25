using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteHttpMVC.DTO
{
    public class PagoUnicoDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de gasto")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de gasto")]
        public int IdTipoDeGasto { get; set; }

        [Display(Name = "Metodo de pago")]
        [Required(ErrorMessage = "Debe seleccionar un metodo de pago")]
        public int MetodoDePago { get; set; }

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "debe ingresar un valor")]
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
        public double Monto { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        public string Descripcion { get; set; }

        [Display(Name = "Fecha de pago")]
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        public DateTime FechaPago { get; set; }

        [Display(Name = "N° de recibo")]
        [Required(ErrorMessage = "Debe ingresar un numero de recibo")]
        public string NumeroRecibo { get; set; }

    }
}
