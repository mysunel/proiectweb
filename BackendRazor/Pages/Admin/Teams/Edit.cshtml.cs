using BackendRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BackendRazor.Pages.Admin.Teams
{
    public class EditModel : PageModel
    {
        private readonly FrbContext _context;

        public EditModel(FrbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; } = null!;

        [BindProperty]
        public Coach Coach { get; set; } = null!;

        [BindProperty]
        public List<Player> Players { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var team = await _context.Teams
                .Include(t => t.IdcoachNavigation)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Idteam == id);

            if (team == null)
                return NotFound();

            Team = team;
            Coach = team.IdcoachNavigation!;
            Players = team.Players.OrderBy(p => p.Idplayer).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            var team = await _context.Teams
                .Include(t => t.IdcoachNavigation)
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Idteam == id);

            if (team == null)
                return NotFound();

            // VALIDARE: pozitii unice
            var duplicatePositions = Players
                .Where(p => !string.IsNullOrWhiteSpace(p.Position))
                .GroupBy(p => p.Position)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicatePositions.Any())
            {
                ModelState.AddModelError(string.Empty,
                    $"Pozitiile trebuie sa fie unice. Duplicate: {string.Join(", ", duplicatePositions)}");
                return Page();
            }

            // ACTUALIZARE TEAM
            team.Name = Team.Name;
            team.City = Team.City;

            // ACTUALIZARE COACH
            var dbCoach = team.IdcoachNavigation!;
            dbCoach.Firstname = Coach.Firstname;
            dbCoach.Lastname = Coach.Lastname;
            dbCoach.Nationality = Coach.Nationality;
            dbCoach.Birthdate = Coach.Birthdate;

            // ACTUALIZARE PLAYERS
            var dbPlayers = team.Players.OrderBy(p => p.Idplayer).ToList();

            if (dbPlayers.Count != Players.Count)
            {
                ModelState.AddModelError(string.Empty,
                    "Numărul de jucători nu corespunde cu datele existente.");
                return Page();
            }

            for (int i = 0; i < dbPlayers.Count; i++)
            {
                var dbP = dbPlayers[i];
                var formP = Players[i];

                dbP.Firstname = formP.Firstname;
                dbP.Lastname = formP.Lastname;
                dbP.Height = formP.Height;
                dbP.Birthdate = formP.Birthdate;
                dbP.Position = formP.Position;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}