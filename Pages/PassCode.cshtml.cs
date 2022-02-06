using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WorkingHoursTracker.Models;

namespace WorkingHoursTracker.Pages;

public class PassCodePageModel : PageModel
{
    private readonly WorkingHoursDbContext _context;
    
    [BindProperty]
    public int AppUserId { get; set; }
    
    [Required]
    [BindProperty]    
    public string PassCode { get; set; } = string.Empty;


    public PassCodePageModel(WorkingHoursDbContext context)
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

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var appUser = await _context.AppUsers.FindAsync(AppUserId);

        if (appUser == null)
        {
            return NotFound();
        }

        if (appUser.PassCode != PassCode)
        {
            return Page();
        }

        return RedirectToPage("./Status", new { id = AppUserId});
    }
}