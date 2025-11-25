namespace ClienteHttpMVC.DTO
{
    public class LogDTO
    {
        public int Id { get; set; }
        public int? TipoDeGastoId { get; set; }
        public string? TipoDeGasto { get; set; }
        public string? Detalle { get; set; }
        public DateTime? Fecha { get; set; }
        public int? UsuarioId { get; set; }
    }
}
