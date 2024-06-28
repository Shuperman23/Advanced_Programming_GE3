namespace ControlBiblioteca.DTOs
{
    public class AutorDto
    {
        public int AutorId { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime? FechaCreacion { get; set; }
    }
}
