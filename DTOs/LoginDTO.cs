using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
