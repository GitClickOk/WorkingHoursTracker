using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingHoursTracker.Models;

public class AppUser
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AppUserId { get; set ;}
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PassCode { get; set; } = string.Empty;
    public WorkingStatus WorkingStatus { get; set; }
}
