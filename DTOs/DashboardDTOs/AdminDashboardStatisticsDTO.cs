namespace GoWork.DTOs.DashboardDTOs
{
    public class AdminDashboardStatisticsDTO
    {
        public int TotalCompanies { get; set; }
        public int TotalFeedbacks { get; set; }
        public int PendingVerificationRequests { get; set; }
        public int UnreadFeedbacks { get; set; }
        public int TotalPublishedJobs { get; set; }
        public int CompaniesRegisteredThisMonth { get; set; }
        public int NewFeedbacksThisWeek { get; set; }
    }
}
