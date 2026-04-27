namespace GoWork.DTOs.ApplicationDTOs
{
    public class ApplicationDTO
    {
        public int ApplicationId { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; } = null!;
        public string CompanyName { get; set; } = null!;
        public string? CompanyLogo { get; set; }
        public string ApplicationStatus { get; set; } = null!;
        public DateTime AppliedDate { get; set; }
        public bool CanWithdraw { get; set; }
    }
}
