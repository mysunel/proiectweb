using BackendRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly FrbContext _context;

        public IndexModel(FrbContext context)
        {
            _context = context;
        }

        public IList<Match> UpcomingMatches { get; set; } = new List<Match>();
        public IList<Match> CompletedMatches { get; set; } = new List<Match>();

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5;

        [BindProperty(SupportsGet = true)]    //pastreaza soortarea si pentru urmatoarele pagini (paginare)
        public string? CurrentSort { get; set; }  

        [BindProperty(SupportsGet = true)]     //pastreaza filtrarea si pentru urmatoarele pagini (paginare)
        public string? CurrentFilter { get; set; } 

        public async Task OnGetAsync(int? p, string sortOrder, string searchString)
        {
            CurrentPage = p ?? 1;
            CurrentSort = sortOrder;
            CurrentFilter = searchString;

            if (_context.Matches != null)
            {
                UpcomingMatches = await _context.Matches
                    .Include(m => m.IdhomeNavigation)
                    .Include(m => m.IdguestNavigation)
                    .Where(m => m.Date.ToDateTime(TimeOnly.MinValue) >= DateTime.Today && (m.Scorehome == null || m.Scoreguest == null))
                    .OrderBy(m => m.Date)
                    .ToListAsync();

                var completedQuery = _context.Matches
                    .Include(m => m.IdhomeNavigation)
                    .Include(m => m.IdguestNavigation)
                    .Where(m => m.Date.ToDateTime(TimeOnly.MinValue) < DateTime.Today || (m.Scorehome != null && m.Scoreguest != null))
                    .AsQueryable();

                if (!string.IsNullOrEmpty(searchString))  //filtrare dupa nume echipa
                {
                    string search = searchString.ToLower();
                    completedQuery = completedQuery.Where(m => 
                        (m.IdhomeNavigation != null && m.IdhomeNavigation.Name.ToLower().Contains(search)) || 
                        (m.IdguestNavigation != null && m.IdguestNavigation.Name.ToLower().Contains(search)));
                }

                if (sortOrder == "date_asc")
                {
                    completedQuery = completedQuery.OrderBy(m => m.Date);
                }
                else
                {
                    completedQuery = completedQuery.OrderByDescending(m => m.Date);
                }

                int totalCompleted = await completedQuery.CountAsync();
                TotalPages = (int)Math.Ceiling(totalCompleted / (double)PageSize);

                if (CurrentPage > TotalPages && TotalPages > 0) CurrentPage = TotalPages;

                CompletedMatches = await completedQuery  //paginare
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync();
            }
        }
    }
}