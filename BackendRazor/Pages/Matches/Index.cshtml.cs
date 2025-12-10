using BackendRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Pages.Matches
{
    public class IndexModel : PageModel
    {

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

        }
          
    }
}