using GoWork.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWork.Models
{
    public class Seeker
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } = null!;
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirsName { get; set; } = null!;
        [Required(ErrorMessage = "Middle Name is required.")]
        [StringLength(50, ErrorMessage = "Middl Name cannot exceed 50 characters.")]
        public string MiddleName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } = null!;
        [StringLength(50, ErrorMessage = "Major cannot exceed 100 characters.")]
        public string? Major { get; set; }
        public string? ResumeUrl { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; } = null!;
        public int GovernateId { get; set; }
        [ForeignKey("GovernateId")]
        public Governate Governate { get; set; } = null!;
        public int InterestCategoryId { get; set; }
        [ForeignKey("InterestCategoryId")]
        public Category InterestCategory { get; set; } = null!;

        //[Required(ErrorMessage = "City is required.")]
        //[StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        //public string City { get; set; } = null!;

        // Navigation
        public ICollection<SeekerSkill> SeekerSkills { get; set; } = null!;
        public ICollection<Application>? Applications { get; set; }

    }

    public class Seeker2
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; } = null!;
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirsName { get; set; } = null!;
        [Required(ErrorMessage = "Middle Name is required.")]
        [StringLength(50, ErrorMessage = "Middl Name cannot exceed 50 characters.")]
        public string MiddleName { get; set; } = null!;
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; } = null!;
        [StringLength(50, ErrorMessage = "Major cannot exceed 100 characters.")]
        public string? Major { get; set; }
        public string? ResumeUrl { get; set; }
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; } = null!;
        public int InterestCategoryId { get; set; }
        [ForeignKey("InterestCategoryId")]
        public Category InterestCategory { get; set; } = null!;

        //[Required(ErrorMessage = "City is required.")]
        //[StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        //public string City { get; set; } = null!;

        // Navigation
        public ICollection<SeekerSkill> SeekerSkills { get; set; } = null!;
        public ICollection<Application>? Applications { get; set; }

    }
}
