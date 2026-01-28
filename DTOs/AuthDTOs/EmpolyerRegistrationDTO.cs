using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.AuthDTOs
{
    public class EmpolyerRegistrationDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string CompanyName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Industry { get; set; }

        //[Url]
        public IFormFile? LogoUrl { get; set; }
    }
}
