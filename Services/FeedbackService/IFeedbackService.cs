using ECommerceApp.DTOs;
using GoWork.DTOs.FeedbackDTOs;

namespace GoWork.Services.FeedbackService
{
    public interface IFeedbackService
    {
        Task<ApiResponse<ConfirmationResponseDTO>> SubmitFeedbackAsync(int userId, SubmitFeedbackDTO dto);
        Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbacksAsync(int? feedbackTypeId = null);
        Task<ApiResponse<List<LookUpDTO>>> GetFeedbackTypesAsync();
        Task<ApiResponse<ConfirmationResponseDTO>> MarkAsReadAsync(int feedbackId);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteFeedbackAsync(int feedbackId);
    }
}
