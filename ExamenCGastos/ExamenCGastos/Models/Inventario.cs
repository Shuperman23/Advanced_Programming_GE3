using System;
using System.Collections.Generic;

namespace ExamenCGastos.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public int? ProductoId { get; set; }

    public string? TipoMovimiento { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public DateTime FechaMovimiento { get; set; }

    public DateTime? FechaCaducidad { get; set; }

    public virtual Producto? Producto { get; set; }
}
