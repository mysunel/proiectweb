using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BackendRazor.Models;

namespace MyApp.Pages.Teams
{
    
    public class IndexModel : PageModel
    {
        private readonly FrbContext _context;

        public IndexModel(FrbContext context)
        {
            _context = context;
        }

        public IList<Team> Teams { get; set; } = new List<Team>();
        public void OnGet()
        {
            Teams = _context.Teams.ToList();
        }
    }
}
