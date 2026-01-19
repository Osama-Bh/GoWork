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
        public ApplicationUser ApplicationUser { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Company Name must be between 3 and 100 characters")]
        public string ComapnyName { get; set; }
        public string LogoUrl { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Industry Name must be between 3 and 100 characters")]
        public string Industry { get; set; }
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Governate Name must be between 3 and 100 characters")]
        public string Governate { get; set; }

        //[StringLength(100, MinimumLength = 3, ErrorMessage = "City Name must be between 3 and 100 characters")]
        //public string City { get; set; }

        [Required]
        [EnumDataType(typeof(EmployerStatus), ErrorMessage = "Invalid Employer Status.")]
        public EmployerStatus Status { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }

    public class Employer2
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; } = null!;

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Company Name must be between 3 and 100 characters")]
        public string ComapnyName { get; set; }
        public string LogoUrl { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Industry Name must be between 3 and 100 characters")]
        public string Industry { get; set; }

        //[StringLength(100, MinimumLength = 3, ErrorMessage = "City Name must be between 3 and 100 characters")]
        //public string City { get; set; }

        [Required]
        [EnumDataType(typeof(EmployerStatus), ErrorMessage = "Invalid Employer Status.")]
        public EmployerStatus Status { get; set; }
        public ICollection<Job>? Jobs { get; set; }
    }
}
