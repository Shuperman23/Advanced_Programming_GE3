using System;
using System.Collections.Generic;

namespace ControlBiblioteca.Models;

public partial class Libro
{
    public int LibroId { get; set; }

    public int GeneroLiterarioId { get; set; }

    public int AutorId { get; set; }

    public string NombreLibro { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public virtual Autor Autor { get; set; } = null!;

    public virtual GeneroLiterario GeneroLiterario { get; set; } = null!;
}
