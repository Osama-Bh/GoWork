using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs
{
    public class CandidateRegistrationDTO
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MidName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Url]
        public string? ProfilePhotoUrl { get; set; }

        [Url]
        public string? ResumeUrl { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one skill is required")]
        public List<string> ListOfSkills { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid category")]
        public int InterstedInCategoryId { get; set; }
    }
}
