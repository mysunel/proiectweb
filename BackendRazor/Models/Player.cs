using System;
using System.Collections.Generic;

namespace BackendRazor.Models;

public partial class Player
{
    public int Idplayer { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public string Position { get; set; } = null!;

    public int Height { get; set; }

    public int Idteam { get; set; }

    public virtual Team IdteamNavigation { get; set; } = null!;

    public virtual ICollection<PlayerMatchStat> PlayerMatchStats { get; set; } = new List<PlayerMatchStat>();
}
