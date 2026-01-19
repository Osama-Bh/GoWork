using System.Text.Json.Serialization;

namespace GoWork.Models
{
    public class JobStatus
    {
        public int Id { get; set; }              // PK, matches enum numeric value
        public string Name { get; set; } = null!;   // "Draft", "Published", "Closed"
        public int SortOrder { get; set; }       // UI display order: 1,2,3...
        public bool IsActive { get; set; } = true;   // Enable/disable without delete

        // Navigation: all jobs with this status
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }

}
