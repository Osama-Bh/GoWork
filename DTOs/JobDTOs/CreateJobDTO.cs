using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.JobDTOs
{
    public class CreateJobDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(700)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int JobTypeId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int JobLocationTypeId { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int GovernateId { get; set; }

        public string? AddressLine { get; set; }

        [Required]
        [Range(0.01, 1000000.00)]
        public decimal MinSalary { get; set; }

        [Required]
        [Range(0.01, 1000000.00)]
        public decimal MaxSalary { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// IDs of existing skills to associate with this job.
        /// </summary>
        public List<int> SkillIds { get; set; } = new();

        /// <summary>
        /// Names of new skills that don't exist yet — will be created automatically.
        /// </summary>
        public List<string> NewSkills { get; set; } = new();
    }
}
