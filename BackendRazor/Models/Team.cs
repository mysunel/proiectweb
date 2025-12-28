using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendRazor.Models;

public partial class Team
{
    public int Idteam { get; set; }

    [Required(ErrorMessage = "Numele echipei este obligatoriu.")]
    [StringLength(100, ErrorMessage = "Numele echipei nu poate depasi 100 de caractere.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Orasul este obligatoriu.")]
    [StringLength(3, ErrorMessage = "Orasul trebuie să aiba maxim 3 litere.")]
    public string? City { get; set; }


    public int Idcoach { get; set; }

    public virtual Coach? IdcoachNavigation { get; set; }

    public virtual ICollection<Match> MatchIdguestNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchIdhomeNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
