using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.DashboardDTOs
{
    public class UpdateCompanyStatusDTO
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
