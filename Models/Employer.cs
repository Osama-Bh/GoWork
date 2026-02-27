using GoWork.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWork.Models
{
    public class Employer
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public Address Address { get; set; } = null!;

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Company Name must be between 3 and 100 characters")]
        public string ComapnyName { get; set; } = null!;
        public int? AddressId { get; set; }
        //[Url(ErrorMessage = "Please enter a valid URL for the company logo.")]
        public string? LogoUrl { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Industry Name must be between 3 and 100 characters")]
        public string Industry { get; set; } = null!;

        public int EmployerStatusId { get; set; }
        [ForeignKey("EmployerStatusId")]
        public EmployerStatus EmployerStatus { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Job>? Jobs { get; set; }
    }
}
