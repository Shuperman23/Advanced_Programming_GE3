namespace ExamenCGastos.DTOs
{
    public class ProveedorDto
    {
        public int IdProveedor { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string TelefonoContacto { get; set; } = null!;
        public string EmailContacto { get; set; } = null!;
    }
}