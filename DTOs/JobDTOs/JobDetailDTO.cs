namespace GoWork.DTOs.JobDTOs
{
    public class JobDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int JobTypeId { get; set; }
        public string JobType { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string Category { get; set; } = string.Empty;
        public int JobLocationTypeId { get; set; }
        public string JobLocationType { get; set; } = string.Empty;
        public int CurrencyId { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ApplicantsCount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string Country { get; set; } = string.Empty;
        public int GovernateId { get; set; }
        public string Governate { get; set; } = string.Empty;
        public List<SkillDTO> Skills { get; set; } = new();
    }

    public class SkillDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
