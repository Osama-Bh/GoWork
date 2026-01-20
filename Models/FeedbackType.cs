using GoWork.Models;

public class FeedbackType
{
    public int Id { get; set; }              // PK, e.g. 1 = Bug, 2 = FeatureRequest
    public int SortOrder { get; set; }
    public string Name { get; set; } = null!;

    // Navigation to children (optional but recommended)
    public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}
