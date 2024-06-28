namespace ExamenCGastos.DTOs
{
    public class InventarioDto
    {
        public int Id { get; set; }
        public int? ProductoId { get; set; }
        public string? TipoMovimiento { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public DateTime? FechaCaducidad { get; set; }

    }
}