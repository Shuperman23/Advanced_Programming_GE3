namespace ControlBiblioteca.DTOs
{
    public class LibroDto
    {
        public int LibroId { get; set; }

        public int GeneroLiterarioId { get; set; }

        public int AutorId { get; set; }

        public string NombreLibro { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public DateTime? FechaCreacion { get; set; }

    }
}
