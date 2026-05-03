using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs;
using GoWork.DTOs.FeedbackDTOs;
using GoWork.Models;
using Microsoft.EntityFrameworkCore;

namespace GoWork.Services.FeedbackService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> SubmitFeedbackAsync(int userId, SubmitFeedbackDTO dto)
        {
            // Validate that the requested feedback type exists in TbFeedbackTypes
            var feedbackTypeId = (int)dto.FeedbackType;

            var typeExists = await _context.TbFeedbackTypes
                .AnyAsync(ft => ft.Id == feedbackTypeId);

            if (!typeExists)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid feedback type.");

            var feedback = new Feedback
            {
                ReviewerId     = userId,
                FeedbackTypeId = feedbackTypeId,
                Message        = dto.Message,
                IsRead         = false,
                CreatedAt      = DateTime.UtcNow
            };

            await _context.TbFeedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200,
                new ConfirmationResponseDTO { Message = "Feedback submitted successfully." });
        }

        public async Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbacksAsync(int? feedbackTypeId = null)
        {
            var query = _context.TbFeedbacks
               .Include(f => f.AppUser)
               .Include(f => f.FeedbackType)
               .AsQueryable();

            if (feedbackTypeId.HasValue)
            {
                query = query.Where(f => f.FeedbackTypeId == feedbackTypeId.Value);
            }

            var feedbacks = await query
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new FeedbackResponseDTO
                {
                    Id = f.Id,
                    
                    ReviewerName = f.AppUser.Name ?? "Unknown",
                    
                    FeedbackTypeName = f.FeedbackType.Name,
                    Message = f.Message,
                    IsRead = f.IsRead,
                    CreatedAt = f.CreatedAt
                })
                .ToListAsync();

            return new ApiResponse<List<FeedbackResponseDTO>>(200, feedbacks);
        }

        public async Task<ApiResponse<List<LookUpDTO>>> GetFeedbackTypesAsync()
        {
            var types = await _context.TbFeedbackTypes
                .OrderBy(t => t.SortOrder)
                .Select(t => new LookUpDTO
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return new ApiResponse<List<LookUpDTO>>(200, types);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> MarkAsReadAsync(int feedbackId)
        {
            var feedback = await _context.TbFeedbacks.FindAsync(feedbackId);
            if (feedback == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Feedback not found.");

            feedback.IsRead = true;
            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200,
                new ConfirmationResponseDTO { Message = "Feedback marked as read." });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(int feedbackId)
        {
            var feedback = await _context.TbFeedbacks.FindAsync(feedbackId);
            if (feedback == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Feedback not found.");

            _context.TbFeedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200,
                new ConfirmationResponseDTO { Message = "Feedback deleted successfully." });
        }
    }
}
