using System;

namespace GoWork.DTOs.FeedbackDTOs
{
    public class FeedbackResponseDTO
    {
        public int Id { get; set; }
        
        public string ReviewerName { get; set; } = null!;
        public string FeedbackTypeName { get; set; } = null!;
        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
