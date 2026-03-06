namespace GoWork.DTOs.JobDTOs
{
    public class JobRecommendationResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public List<string> RequiredSkills { get; set; } = new();
        public double? Score { get; set; }
    }
}
