using BackendRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyApp.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly FrbContext _context;

        public DetailsModel(FrbContext context)
        {
            _context = context;
        }

        public Team Team { get; set; } = new Team();
        public Coach Coach { get; set; } = new Coach();
        public List<Player> Players { get; set; } = new List<Player>();

        public IActionResult OnGet(int id)
        {
            Team = _context.Teams.FirstOrDefault(t => t.Idteam == id)!;

            if (Team == null)
            {
                return NotFound();
            }

            // Coach-ul asociat echipei
            Coach = _context.Coaches.FirstOrDefault(c => c.Idcoach == Team.Idcoach)!;

            // Lista de jucători ai echipei
            Players = _context.Players.Where(p => p.Idteam == id).ToList();

            return Page();

        }
    }
}

