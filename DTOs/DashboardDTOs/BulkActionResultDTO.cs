namespace GoWork.DTOs.DashboardDTOs
{
    public class BulkActionResultDTO
    {
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
