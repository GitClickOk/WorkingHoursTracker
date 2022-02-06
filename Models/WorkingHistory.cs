using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingHoursTracker.Models;

public class WorkingHistory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int WorkingHistoryId { get; set; }
    public int AppUserId { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
}