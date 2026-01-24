using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.AuthDTOs
{
    public class EmailConfirmationDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(6)]
        public string EmailConfirmationCode { get; set; } = string.Empty;
    }
}