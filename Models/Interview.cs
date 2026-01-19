using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWork.Models
{
    public class Interview
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; } = null!;
        public DateTime InterviewDate { get; set; }
        public int InterviewTypeId { get; set; }
        [ForeignKey("InterviewTypeId")]
        public InterviewType InterviewType { get; set; } = null!;
        public string Locatoin { get; set; }
        [StringLength(200, ErrorMessage = "Notes cannot exceed 100 characters.")]
        public string? Notes { get; set; }
        public int InterviewStatusId { get; set; }
        public InterviewStatus InterviewStatus { get; set; }

    }
}
