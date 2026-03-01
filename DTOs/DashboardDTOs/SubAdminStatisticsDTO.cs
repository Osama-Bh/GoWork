namespace GoWork.DTOs.DashboardDTOs
{
    public class SubAdminStatisticsDTO
    {
        public int TotalSubAdmins { get; set; }
        public int ActiveSubAdmins { get; set; }
        public int SuspendedSubAdmins { get; set; }
        public int BlockedSubAdmins { get; set; }
    }
}
