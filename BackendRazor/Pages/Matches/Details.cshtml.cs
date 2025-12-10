using BackendRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages.Matches
{
    public class DetailsModel : PageModel
    {
        private readonly FrbContext _context;

        public DetailsModel(FrbContext context)
        {
            _context = context;
        }

       public Match? Match { get; set; }
        
        public IList<PlayerMatchStat> HomeTeamStats { get; set; } = new List<PlayerMatchStat>();
        public IList<PlayerMatchStat> GuestTeamStats { get; set; } = new List<PlayerMatchStat>();

        public Team HomeTeam { get; set; } = new Team();
        public Team GuestTeam { get; set; } = new Team();


        public async Task<IActionResult> OnGetAsync(int id) 
        {
            if (_context == null || _context.Matches == null) return NotFound();

            
            Match = await _context.Matches
                .Include(m => m.IdhomeNavigation) 
                .Include(m => m.IdguestNavigation) 
                .FirstOrDefaultAsync(m => m.Idmatch == id);

            if (Match == null)
            {
                return NotFound();
            }

            HomeTeam = (await _context.Teams.FirstOrDefaultAsync(t => t.Idteam == Match.Idhome))!;
            GuestTeam = (await _context.Teams.FirstOrDefaultAsync(t => t.Idteam == Match.Idguest))!;

            var allStats = await _context.PlayerMatchStats
                .Include(s => s.IdplayerNavigation) 
                .Where(s => s.Idmatch == id)
                .ToListAsync();

            HomeTeamStats = allStats
                .Where(s => s.IdplayerNavigation != null && s.IdplayerNavigation.Idteam == Match.Idhome)
                .ToList();
            
            GuestTeamStats = allStats
                .Where(s => s.IdplayerNavigation != null && s.IdplayerNavigation.Idteam == Match.Idguest)
                .ToList();

            return Page();
        }
    }
}