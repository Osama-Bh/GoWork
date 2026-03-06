namespace GoWork.DTOs.JobDTOs
{
    public class PreFilteredJobDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public DateTime PostedDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? RequiredSkills { get; set; }
    }
}
