using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.AuthDTOs;

namespace GoWork.Service.AccountService
{
    public interface IAccountService
    {
        Task<ApiResponse<CandidateRegistrationResponseDTO>> CandidateRegisterAsync(
            CandidateRegistrationDTO registrationDTO
        );

        Task<ApiResponse<ConfirmationResponseDTO>> VerifyEmail(
            EmailConfirmationDTO confirmationDTO
        );

        Task<ApiResponse<EmployerResponseDTO>> VerifyCompanyEmail(EmailConfirmationDTO confirmationDTO);

        Task<ApiResponse<LoginResponseDTO>> Login(
            LoginDTO loginDTO
        );

        Task<ApiResponse<EmployerResponseDTO>> LoginCompany(
            LoginDTO loginDTO
        );

        Task<ApiResponse<ConfirmationResponseDTO>> RegisterCompany(EmpolyerRegistrationDTO registrationDTO);
        string GenerateJwtToken(ApplicationUser? user);
    }

}
