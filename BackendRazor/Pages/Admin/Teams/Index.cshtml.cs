using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
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

    public async Task OnGetAsync()
    {
        Teams = await _context.Teams
            .Include(t => t.IdcoachNavigation)
            .ToListAsync();
    }
}