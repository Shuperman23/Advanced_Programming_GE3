using System;
using System.Collections.Generic;

namespace ControlBiblioteca.Models;

public partial class Parametro
{
    public int ParametroId { get; set; }

    public string Nombre { get; set; } = null!;

    public string Valor { get; set; } = null!;
}
