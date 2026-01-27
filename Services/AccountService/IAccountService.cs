using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.AuthDTOs;

namespace GoWork.Service.AccountService
{
    public interface IAccountService
    {
        // Candidate
        Task<ApiResponse<ConfirmationResponseDTO>> CandidateRegisterAsync(
            CandidateRegistrationDTO registrationDTO
        );

        Task<ApiResponse<CandidateResponseDTO>> VerifyEmail(
            EmailConfirmationDTO confirmationDTO
        );

        Task<ApiResponse<LoginResponseDTO>> Login(
            LoginDTO loginDTO
        );

        // Employer
        Task<ApiResponse<ConfirmationResponseDTO>> RegisterCompany(
            EmpolyerRegistrationDTO registrationDTO
        );

        Task<ApiResponse<EmployerResponseDTO>> VerifyCompanyEmail(
            EmailConfirmationDTO confirmationDTO
        );

        Task<ApiResponse<EmployerResponseDTO>> LoginCompany(
            LoginDTO loginDTO
        );

        // Token
        string GenerateJwtToken(ApplicationUser user);
    }

}
