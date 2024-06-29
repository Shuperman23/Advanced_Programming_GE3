namespace ExamenCGastos.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int? ProveedorId { get; set; }
    }
}