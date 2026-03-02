namespace GoWork.DTOs.JobDTOs
{
    public class JobListItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string JobType { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ApplicantsCount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
