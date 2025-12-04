using System;
using System.Collections.Generic;

namespace BackendRazor.Models;

/// <summary>
/// tabela antrenori
/// </summary>
public partial class Coach
{
    public int Idcoach { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public virtual Team? Team { get; set; }
}
