using GoWork.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWork.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        [ForeignKey("ReviewerId")]
        public ApplicationUser AppUser { get; set; } = null!;
        public int FeedbackTypeId { get; set; }
        [ForeignKey("FeedbackTypeId")]
        public FeedbackType FeedbackType { get; set; } = null!;
        [Required(ErrorMessage = "Message is required.")]
        [StringLength(300, ErrorMessage = "Message cannot exceed 300 characters.")]
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
