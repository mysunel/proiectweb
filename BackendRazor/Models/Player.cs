using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackendRazor.Models;

public partial class Player
{
    public int Idplayer { get; set; }

    [Required(ErrorMessage = "Prenumele este obligatoriu.")]

    public string Firstname { get; set; } = null!;

    [Required(ErrorMessage = "Numele este obligatoriu.")]

    public string Lastname { get; set; } = null!;

    [Required(ErrorMessage = "Data nasterii este obligatorie.")]

    public DateOnly Birthdate { get; set; }

    [Required(ErrorMessage = "Pozitia este obligatorie.")]

    public string Position { get; set; } = null!;

    [Range(150, 250, ErrorMessage = "Inaltimea trebuie sa fie intre 150 si 250 cm.")]
    public int Height { get; set; }

    public int Idteam { get; set; }

    public virtual Team? IdteamNavigation { get; set; }

    public virtual ICollection<PlayerMatchStat> PlayerMatchStats { get; set; } = new List<PlayerMatchStat>();
}
