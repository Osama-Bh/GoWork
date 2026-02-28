using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.DashboardDTOs
{
    public class UpdateSubAdminStatusDTO
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
