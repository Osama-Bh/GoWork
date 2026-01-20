namespace GoWork.Models
{
    public class InterviewType
    {
        public int Id { get; set; }              // PK, matches enum numeric value
        public string Name { get; set; } = null!;   // e.g. "Online", "InPerson", "Phone"
        public int SortOrder { get; set; }
        public bool IsActive { get; set; } = true;   // soft-delete / enable-disable

        // Navigation: all interviews of this type
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
    }

}