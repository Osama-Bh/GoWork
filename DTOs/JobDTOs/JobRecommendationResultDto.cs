namespace GoWork.DTOs.JobDTOs
{
    public class JobRecommendationResultDto
    {
        public string SeekerFullName { get; set; } = string.Empty;
        public int TotalApplicationsCount { get; set; } = 0;
        public int PendingReviewApplicationsCount { get; set; } = 0;
        public int TotalInterviewsCount { get; set; } = 0;
        public List<JobCardDto> Recommendations { get; set; } = new();
    }
}
