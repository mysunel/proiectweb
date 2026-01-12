using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BackendRazor.Pages.Admin.Matches
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
        public Match Match { get; set; } = new Match();

        public SelectList TeamOptions { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync() //populare dopdown uri echipe
        {

            var teams = await _context.Teams.OrderBy(t => t.Name).ToListAsync(); 
            TeamOptions = new SelectList(teams, "Idteam", "Name");

            Match.Date = DateOnly.FromDateTime(DateTime.Today);

            return Page();
        }

       public async Task<IActionResult> OnPostAsync()
        {

            if (Match.Date < DateOnly.FromDateTime(DateTime.Today)) //validare data
            {
                ModelState.AddModelError("Match.Date", "Data nu poate fi în trecut.");
            }

            if (Match.Idhome == Match.Idguest) //validare echipe diferite
            {
                ModelState.AddModelError(string.Empty, "Echipele trebuie să fie diferite.");
            }

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors) 
                {
                    Console.WriteLine($"Eroare Validare: {error.ErrorMessage}");
                }

                var teams = await _context.Teams.OrderBy(t => t.Name).ToListAsync();
                TeamOptions = new SelectList(teams, "Idteam", "Name");
                return Page();
            }

            try 
            {
                _context.Matches.Add(Match);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eroare Baza de Date: {ex.Message}");
                ModelState.AddModelError(string.Empty, "Eroare la salvare: " + ex.Message);
                
                var teams = await _context.Teams.OrderBy(t => t.Name).ToListAsync();
                TeamOptions = new SelectList(teams, "Idteam", "Name");
                return Page();
            }
        }
    }
}