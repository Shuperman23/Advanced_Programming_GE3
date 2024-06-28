using System;
using System.Collections.Generic;

namespace ControlBiblioteca.Models;

public partial class GeneroLiterario
{
    public int GeneroLiterarioID { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
