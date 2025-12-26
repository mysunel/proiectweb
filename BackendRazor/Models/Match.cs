using System;
using System.Collections.Generic;

namespace BackendRazor.Models;

public partial class Match
{
    public int Idmatch { get; set; }

    public DateOnly Date { get; set; }

    public int? Scorehome { get; set; }

    public int? Scoreguest { get; set; }

    public int Idhome { get; set; }

    public int Idguest { get; set; }

    public virtual Team? IdguestNavigation { get; set; } = null!;

    public virtual Team? IdhomeNavigation { get; set; } = null!;

    public virtual ICollection<PlayerMatchStat> PlayerMatchStats { get; set; } = new List<PlayerMatchStat>();
}
