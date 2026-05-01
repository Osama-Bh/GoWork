namespace GoWork.DTOs.DashboardDTOs
{
    public class CompanyApplicationsFilterDTO
    {
        public string? SearchTerm { get; set; }
        public int? JobId { get; set; }
        public int? StatusId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
