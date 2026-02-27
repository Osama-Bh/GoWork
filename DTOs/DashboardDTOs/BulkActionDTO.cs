using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.DashboardDTOs
{
    public class BulkActionDTO
    {
        [Required]
        public List<int> CompanyIds { get; set; } = new();

        [Required]
        public string Action { get; set; } = string.Empty; // "Approve", "Reject", "Delete", "Suspend"
    }
}
