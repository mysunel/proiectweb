using System;
using System.Collections.Generic;

namespace BackendRazor.Models;

public partial class PlayerMatchStat
{
    public int Idpms { get; set; }

    public int? Points { get; set; }

    public int? Rebounds { get; set; }

    public int? Assists { get; set; }

    public int Idplayer { get; set; }

    public int Idmatch { get; set; }

    public virtual Match? IdmatchNavigation { get; set; } = null!;

    public virtual Player? IdplayerNavigation { get; set; } = null!;
}
