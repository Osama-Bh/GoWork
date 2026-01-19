using GoWork.Models;

public class JobType
{
    public int Id { get; set; }              // 1 = FullTime, 2 = PartTime, ...
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public ICollection<Job> Jobs { get; set; } = null!;
}
