using System;
using System.Collections.Generic;

namespace ExamenCGastos.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? ProveedorId { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual Proveedor? Proveedor { get; set; }
}
