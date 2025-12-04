using System;
using System.Collections.Generic;

namespace BackendRazor.Models;

public partial class Team
{
    public int Idteam { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public int Idcoach { get; set; }

    public virtual Coach IdcoachNavigation { get; set; } = null!;

    public virtual ICollection<Match> MatchIdguestNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchIdhomeNavigations { get; set; } = new List<Match>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
