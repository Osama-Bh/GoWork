using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.DashboardDTOs
{
    public class AdminUpdateCompanyDTO
    {
        [StringLength(100, MinimumLength = 3)]
        public string? CompanyName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? Industry { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
