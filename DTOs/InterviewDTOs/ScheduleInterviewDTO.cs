using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.InterviewDTOs
{
    public class ScheduleInterviewDTO
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public DateTime InterviewDate { get; set; }

        [Required]
        public int InterviewTypeId { get; set; }

        [MaxLength(200)]
        public string? Notes { get; set; }

        [MaxLength(500)]
        public string? MeetingLink { get; set; }

        [MaxLength(200)]
        public string? AddressLine1 { get; set; }

        public int? CountryId { get; set; }

        public int? GovernateId { get; set; }
    }
}
