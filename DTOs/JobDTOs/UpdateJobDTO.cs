using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.JobDTOs
{
    public class UpdateJobDTO
    {
        [StringLength(100, MinimumLength = 2)]
        public string? Title { get; set; }

        [StringLength(700)]
        public string? Description { get; set; }

        public int? JobTypeId { get; set; }
        public int? CategoryId { get; set; }
        public int? JobLocationTypeId { get; set; }
        public int? CurrencyId { get; set; }
        public int? CountryId { get; set; }
        public int? GovernateId { get; set; }
        public string? AddressLine { get; set; }

        [Range(0.01, 1000000.00)]
        public decimal? MinSalary { get; set; }

        [Range(0.01, 1000000.00)]
        public decimal? MaxSalary { get; set; }

        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Replace existing skills with these IDs.
        /// </summary>
        public List<int>? SkillIds { get; set; }

        /// <summary>
        /// New skills to create and associate.
        /// </summary>
        public List<string>? NewSkills { get; set; }
    }
}
