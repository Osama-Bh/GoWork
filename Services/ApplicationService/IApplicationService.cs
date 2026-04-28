using ECommerceApp.DTOs;
using GoWork.DTOs;
using GoWork.DTOs.ApplicationDTOs;

namespace GoWork.Services.ApplicationService
{
    public interface IApplicationService
    {
        public Task<ApiResponse<ApplicationsResponseDTO>> GetCandidateApplications(ApplicationsRequestDTO requestDTO);
        public Task<ApiResponse<List<LookUpDTO>>> GetApplicationStatuses();
        public Task<ApiResponse<ConfirmationResponseDTO>> WithdrawApplicationAsync(int applicationId, int userId);

    }
}
