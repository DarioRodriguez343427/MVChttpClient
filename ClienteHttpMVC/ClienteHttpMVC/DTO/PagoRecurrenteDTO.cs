using System.ComponentModel.DataAnnotations;


namespace ClienteHttpMVC.DTO
{
    public class PagoRecurrenteDTO
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de gasto")]
        [Required(ErrorMessage = "Debe seleccionar un tipo de pago")]
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
        [Display(Name = "Fecha desde")]
        public DateTime FechaDesde { get; set; }
        [Display(Name = "Fecha hasta")]
        public DateTime FechaHasta { get; set; }

    }
}
