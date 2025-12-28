using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BackendRazor.Pages.Admin.Teams
{
    [Authorize]
        public class CreateModel : PageModel
        {
            private readonly FrbContext _context;

        public CreateModel(FrbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Team Team { get; set; } = new Team();

        [BindProperty]
        public Coach Coach { get; set; } = new Coach();

        [BindProperty]
        public List<Player> Players { get; set; } = new()
        {
            new Player(),
            new Player(),
            new Player(),
            new Player(),
            new Player()
        };

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

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
                    $"Pozițiile trebuie să fie unice. Duplicate: {string.Join(", ", duplicatePositions)}");
                return Page();
            }

            _context.Coaches.Add(Coach);

            Team.IdcoachNavigation = Coach;
            _context.Teams.Add(Team);
            
            foreach (var p in Players)
            {
                p.IdteamNavigation = Team;
                _context.Players.Add(p);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }


    }
}
