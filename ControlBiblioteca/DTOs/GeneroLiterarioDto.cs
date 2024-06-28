namespace ControlBiblioteca.DTOs
{
    public class GeneroLiterarioDto
    {
        public int GeneroLiterarioID { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime? FechaCreacion { get; set; }
    }
}