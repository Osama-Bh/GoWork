using GoWork.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoWork.DTOs.FeedbackDTOs
{
    public class SubmitFeedbackDTO
    {
        [Required(ErrorMessage = "Feedback type is required.")]
        public FeedbackTypeEnum FeedbackType { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Message must be between 10 and 300 characters.")]
        public string Message { get; set; } = null!;
    }
}
