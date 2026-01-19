using GoWork.Models;

public class JobLocationType
{
    public int Id { get; set; }              // PK, matches enum numeric value
    public string Name { get; set; } = null!;   // e.g. "On-site", "Remote", "Hybrid"
    public bool IsActive { get; set; } = true;   // soft-delete / enable-disable

    // Navigation: all jobs of this location type
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}
