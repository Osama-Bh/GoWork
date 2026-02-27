namespace GoWork.DTOs.DashboardDTOs
{
    public class CompanyListItemDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
    }
}
