using System;
using System.Collections.Generic;

namespace ExamenCGastos.Models;

public partial class ErrorLog
{
    public int Id { get; set; }

    public string Controller { get; set; } = null!;

    public string Endpoint { get; set; } = null!;

    public string ErrorMessage { get; set; } = null!;

    public string ErrorStackTrace { get; set; } = null!;

    public DateTime ErrorTimestamp { get; set; }
}
