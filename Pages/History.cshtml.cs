using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WorkingHoursTracker.Models;

namespace WorkingHoursTracker.Pages;

public class HistoryPageModel : PageModel
{
    private readonly WorkingHoursDbContext _context;

    public int AppUserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public IEnumerable<WorkingHistory> History { get; set; } = Enumerable.Empty<WorkingHistory>();

    public HistoryPageModel(WorkingHoursDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var appUser = await _context.AppUsers.FindAsync(id);

        if (appUser == null)
        {
            return NotFound();
        }

        AppUserId = appUser.AppUserId;
        FirstName = appUser.FirstName;
        LastName = appUser.LastName;

        History = await _context
            .WorkingHistory
            .Where(x => x.AppUserId == AppUserId)
            .OrderByDescending(x => x.WorkingHistoryId)
            .ToListAsync();

        return Page();
    }
}