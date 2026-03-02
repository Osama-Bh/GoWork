using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.JobDTOs
{
    public class UpdateJobStatusDTO
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
