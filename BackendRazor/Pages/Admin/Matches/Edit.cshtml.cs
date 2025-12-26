using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BackendRazor.Pages.Admin.Matches
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly FrbContext _context;
        public EditModel(FrbContext context) => _context = context;

        [BindProperty]
        public Match Match { get; set; } = default!;

        public List<Player> HomePlayers { get; set; } = new();
        public List<Player> GuestPlayers { get; set; } = new();

        [BindProperty]
        public Dictionary<int, PlayerMatchStat> PlayerStats { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var matchData = await _context.Matches
                .Include(m => m.IdhomeNavigation!).ThenInclude(t => t.Players)
                .Include(m => m.IdguestNavigation!).ThenInclude(t => t.Players)
                .Include(m => m.PlayerMatchStats)
                .FirstOrDefaultAsync(m => m.Idmatch == id);

            if (matchData == null) return NotFound();
            Match = matchData;

            HomePlayers = Match.IdhomeNavigation?.Players.ToList() ?? new List<Player>();
            GuestPlayers = Match.IdguestNavigation?.Players.ToList() ?? new List<Player>();

            foreach (var player in HomePlayers.Concat(GuestPlayers))
            {
                var stat = Match.PlayerMatchStats.FirstOrDefault(s => s.Idplayer == player.Idplayer);
                PlayerStats[player.Idplayer] = stat ?? new PlayerMatchStat { Idplayer = player.Idplayer, Idmatch = id };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {

            var dbMatch = await _context.Matches
                .Include(m => m.IdhomeNavigation).ThenInclude(t => t!.Players)
                .Include(m => m.IdguestNavigation).ThenInclude(t => t!.Players)
                .Include(m => m.PlayerMatchStats)
                .FirstOrDefaultAsync(m => m.Idmatch == id);

            if (dbMatch == null) return NotFound();


            HomePlayers = dbMatch.IdhomeNavigation?.Players.ToList() ?? new();
            GuestPlayers = dbMatch.IdguestNavigation?.Players.ToList() ?? new();

            int sumPointsHome = HomePlayers.Sum(p => PlayerStats.ContainsKey(p.Idplayer) ? (PlayerStats[p.Idplayer].Points ?? 0) : 0);
            int sumPointsGuest = GuestPlayers.Sum(p => PlayerStats.ContainsKey(p.Idplayer) ? (PlayerStats[p.Idplayer].Points ?? 0) : 0);

            if (Match.Scorehome != sumPointsHome)
                ModelState.AddModelError("Match.Scorehome", $"Suma punctelor gazdă ({sumPointsHome}) != {Match.Scorehome}");
            if (Match.Scoreguest != sumPointsGuest)
                ModelState.AddModelError("Match.Scoreguest", $"Suma punctelor oaspeți ({sumPointsGuest}) != {Match.Scoreguest}");

            ModelState.Remove("Match.IdhomeNavigation");
            ModelState.Remove("Match.IdguestNavigation");
            foreach (var key in PlayerStats.Keys) {
                ModelState.Remove($"PlayerStats[{key}].IdmatchNavigation");
                ModelState.Remove($"PlayerStats[{key}].IdplayerNavigation");
            }

            if (!ModelState.IsValid) return Page();

            dbMatch.Date = Match.Date;
            dbMatch.Scorehome = Match.Scorehome;
            dbMatch.Scoreguest = Match.Scoreguest;

            foreach (var entry in PlayerStats)
            {
                var playerId = entry.Key;
                var submittedStat = entry.Value;

                var existingStat = dbMatch.PlayerMatchStats.FirstOrDefault(s => s.Idplayer == playerId);

                if (existingStat != null)
                {

                    existingStat.Points = submittedStat.Points;
                    existingStat.Rebounds = submittedStat.Rebounds;
                    existingStat.Assists = submittedStat.Assists;
                }
                else if (submittedStat.Points.HasValue || submittedStat.Rebounds.HasValue || submittedStat.Assists.HasValue)
                {

                    _context.PlayerMatchStats.Add(new PlayerMatchStat
                    {
                        Idmatch = id,
                        Idplayer = playerId,
                        Points = submittedStat.Points,
                        Rebounds = submittedStat.Rebounds,
                        Assists = submittedStat.Assists
                    });
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}