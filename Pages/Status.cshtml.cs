using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkingHoursTracker.Models;

namespace WorkingHoursTracker.Pages;

public class StatusPageModel : PageModel
{
    private readonly WorkingHoursDbContext _context;

    [BindProperty]
    public int AppUserId { get; set; }

    public WorkingStatus WorkingStatus { get; set; }

    public StatusPageModel(WorkingHoursDbContext context)
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
        WorkingStatus = appUser.WorkingStatus;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var appUser = await _context.AppUsers.FindAsync(AppUserId);

        if (appUser == null)
        {
            return NotFound();
        }

        appUser.WorkingStatus = appUser.WorkingStatus == WorkingStatus.Working ? WorkingStatus.Out : WorkingStatus.Working;
        if (appUser.WorkingStatus == WorkingStatus.Working)
        {
            var history = new WorkingHistory
            {
                AppUserId = appUser.AppUserId,
                Start = DateTime.Now
            };
            _context.WorkingHistory.Add(history);
        }
        else
        {
            var history = _context
                .WorkingHistory
                .Where(x => x.AppUserId == AppUserId
                       && x.End == null)
                .OrderByDescending(x => x.WorkingHistoryId)
                .FirstOrDefault();

            if (history != null)
            {
                history.End = DateTime.Now;
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

}