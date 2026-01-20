using GoWork.Models;

public class JobLocationType
{
    public int Id { get; set; }              // PK, matches enum numeric value
    public string Name { get; set; } = null!;   // e.g. "On-site", "Remote", "Hybrid"
    public int SortOrder { get; set; } = 0;    // order in which to display location types
    public bool IsActive { get; set; } = true;   // soft-delete / enable-disable

    // Navigation: all jobs of this location type
    public ICollection<Job> Jobs { get; set; } = new List<Job>();
}
