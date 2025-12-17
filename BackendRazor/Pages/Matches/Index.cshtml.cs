using BackendRazor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly FrbContext _context;

        public IndexModel(FrbContext context)
        {
            _context = context;
        }

        public IList<Match> UpcomingMatches { get; set; } = new List<Match>();
        public IList<Match> CompletedMatches { get; set; } = new List<Match>();

        public async Task OnGetAsync()
        {
            if (_context.Matches != null)
            {
                var allMatches = await _context.Matches
                    .Include(m => m.IdhomeNavigation) 
                    .Include(m => m.IdguestNavigation) 
                    .OrderBy(m => m.Date) 
                    .ToListAsync();

                UpcomingMatches = allMatches
                    .Where(m => m.Date.ToDateTime(TimeOnly.MinValue) >= DateTime.Today && (m.Scorehome == null || m.Scoreguest == null))
                    .ToList();

                CompletedMatches = allMatches
                    .Where(m => m.Date.ToDateTime(TimeOnly.MinValue) < DateTime.Today || (m.Scorehome != null && m.Scoreguest != null))
                    .ToList();
            }
        }
    }
}