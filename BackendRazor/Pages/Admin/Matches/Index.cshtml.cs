using BackendRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? SortOrder { get; set; }

    [BindProperty(SupportsGet = true)]
    public DateOnly? FilterDate { get; set; }

    public async Task OnGetAsync(int? p)
    {
        CurrentPage = p ?? 1;

        var query = _context.Matches
            .Include(m => m.IdhomeNavigation)
            .Include(m => m.IdguestNavigation)
            .AsQueryable();

        if (!string.IsNullOrEmpty(SearchString))
        {
            string search = SearchString.ToLower();
            query = query.Where(m => 
                (m.IdhomeNavigation != null && m.IdhomeNavigation.Name.ToLower().Contains(search)) || 
                (m.IdguestNavigation != null && m.IdguestNavigation.Name.ToLower().Contains(search)));
        }

        if (FilterDate.HasValue)
        {
            query = query.Where(m => m.Date == FilterDate.Value);
        }

        query = SortOrder == "date_asc" ? query.OrderBy(m => m.Date) : query.OrderByDescending(m => m.Date);

        int totalMatches = await query.CountAsync();
        TotalPages = (int)Math.Ceiling(totalMatches / (double)PageSize);

        Matches = await query
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match != null)
        {
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
}