using System.Text.Json.Serialization;

namespace GoWork.Models
{
    public class InterviewStatus
    {
        public int Id { get; set; }              // PK, matches enum: 1=Scheduled, etc.
        public string Name { get; set; } = null!;   // "Scheduled", "Completed", "Cancelled"
        public int SortOrder { get; set; }       // UI display order: 1,2,3...
        public bool IsActive { get; set; } = true;   // Enable/disable without delete

        // Navigation: all interviews with this status
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }

}