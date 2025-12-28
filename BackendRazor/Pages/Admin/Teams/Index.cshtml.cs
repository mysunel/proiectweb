using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BackendRazor.Pages.Admin.Teams;
[Authorize]
public class IndexModel : PageModel
{
    private readonly FrbContext _context;

    public IndexModel(FrbContext context)
    {
        _context = context;
    }

    public List<Team> Teams { get; set; } = new();

    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    private const int PageSize = 10;

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }



    public async Task OnGetAsync(int pageNumber = 1)
    {
        PageNumber = pageNumber;

        var query = _context.Teams.AsQueryable();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            query = query.Where(t => t.Name.ToLower().Contains(SearchTerm.ToLower()));
        }

        var totalTeams = await query.CountAsync();

        TotalPages = (int)Math.Ceiling(totalTeams / (double)PageSize);
        if (TotalPages < 1)
            TotalPages = 1;

        if (PageNumber < 1)
            PageNumber = 1;

        if (PageNumber > TotalPages)
            PageNumber = TotalPages;

        Teams = await query
            .OrderBy(t => t.Name)
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team != null)
        {
            // eroare fk
            bool hasMatches = await _context.Matches
                .AnyAsync(m => m.Idguest == id || m.Idhome == id);

            if (hasMatches)
            {
                TempData["ErrorMessage"] = "Echipa nu poate fi stearsa deoarece are meciuri jucate.";
                return RedirectToPage("Index");
            }

            if(team.IdcoachNavigation != null)
                _context.Coaches.Remove(team.IdcoachNavigation);
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage(new { pageNumber = PageNumber });
    }
}