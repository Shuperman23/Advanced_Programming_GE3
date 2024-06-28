using System;
using System.Collections.Generic;

namespace ControlBiblioteca.Models;

public partial class Autor
{
    public int AutorId { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
