namespace GoWork.Models
{
    public class ApplicationStatus
    {
        public int Id { get; set; }              // PK, matches enum: 1=PendingReview, etc.
        public string Name { get; set; } = null!;   // "Pending Review", "Shortlisted"
        public int SortOrder { get; set; }       // UI display order: 1,2,3...
        public bool IsActive { get; set; } = true;   // Enable/disable without delete

        // Navigation: all applications with this status
        public ICollection<Application> Applications { get; set; } = new List<Application>();
    }

}