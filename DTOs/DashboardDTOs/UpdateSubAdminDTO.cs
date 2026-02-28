using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.DashboardDTOs
{
    public class UpdateSubAdminDTO
    {
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
