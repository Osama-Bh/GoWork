namespace GoWork.DTOs.DashboardDTOs
{
    public class CompanyStatisticsDTO
    {
        public int TotalCompanies { get; set; }
        public int PendingVerification { get; set; }
        public int Verified { get; set; }
        public int Rejected { get; set; }
    }
}
