using System;
using System.Collections.Generic;

namespace ExamenCGastos.Models;

public partial class Proveedor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? TelefonoContacto { get; set; }

    public string? EmailContacto { get; set; }

    public string? Direccion { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
