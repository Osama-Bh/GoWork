namespace GoWork.DTOs.JobDTOs
{
    public class JobDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public string JobLocationType { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? Country { get; set; }
        public List<string> Skills { get; set; } = new();
        public bool CanApply { get; set; }
        
        public JobDetailsCompanyDto Company { get; set; } = new();
    }

    public class JobDetailsCompanyDto
    {
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
    }
}
