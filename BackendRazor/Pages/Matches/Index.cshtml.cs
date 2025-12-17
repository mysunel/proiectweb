using BackendRazor.Models;
<<<<<<< HEAD
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
=======
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.EntityFrameworkCore;
>>>>>>> fb6965da51dfe1ea72503da23c18bd9479fb58f0

namespace MyApp.Pages.Matches
{
    public class IndexModel : PageModel
    {
<<<<<<< HEAD
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
=======

        private readonly FrbContext _context;

        public IndexModel(FrbContext context)
        {
            _context = context;
        }

        public IList<Match> Matches { get; set; } = new List<Match>();

        public void OnGet()
        {

            Matches = _context.Matches.ToList();

            Matches = _context.Matches
                .Include(m => m.IdhomeNavigation)  
                .Include(m => m.IdguestNavigation) 
                .ToList();

>>>>>>> fb6965da51dfe1ea72503da23c18bd9479fb58f0
        }
          
    }
}