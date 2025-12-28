using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendRazor.Models;

/// <summary>
/// tabela antrenori
/// </summary>
public partial class Coach
{
    public int Idcoach { get; set; }

    [Required(ErrorMessage = "Prenumele este obligatoriu.")]
    public string Firstname { get; set; } = null!;

    [Required(ErrorMessage = "Numele este obligatoriu.")]
    public string Lastname { get; set; } = null!;

    [Required(ErrorMessage = "Nationalitatea este obligatorie.")]
    public string Nationality { get; set; } = null!;

    [Required(ErrorMessage = "Data nasterii este obligatorie.")]
    public DateOnly Birthdate { get; set; }


    public virtual Team? Team { get; set; }
}
