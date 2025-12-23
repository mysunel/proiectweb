using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BackendRazor.Pages.Admin.Matches;

[Authorize]
public class IndexModel : PageModel
{
    private readonly FrbContext _context;

    public IndexModel(FrbContext context)
    {
        _context = context;
    }

    public List<Match> Matches { get; set; } = new();

    public async Task OnGetAsync()
    {
        Matches = await _context.Matches
            .Include(m => m.IdhomeNavigation)
            .Include(m => m.IdguestNavigation)
            .ToListAsync();
    }
}