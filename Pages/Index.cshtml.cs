using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkingHoursTracker.Models;

namespace WorkingHoursTracker.Pages;

public class IndexPageModel : PageModel
{
    private readonly WorkingHoursDbContext _context;
    private readonly ILogger<IndexPageModel> _logger;

    [BindProperty]
    public IEnumerable<AppUser> AppUsers { get; set; } = Enumerable.Empty<AppUser>();

    public IndexPageModel(WorkingHoursDbContext context, ILogger<IndexPageModel> logger)
    {
        _logger = logger;
        _context = context;
    }

    public async Task OnGetAsync()
    {
        AppUsers = await _context.AppUsers.ToListAsync();
    }
}
