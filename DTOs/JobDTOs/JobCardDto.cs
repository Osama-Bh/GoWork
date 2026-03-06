namespace GoWork.DTOs.JobDTOs
{
    public class JobCardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public string LocationType { get; set; } = string.Empty;
        
        // Can be null if the target Job didn't have an Address
        public string? Country { get; set; } 
        
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
