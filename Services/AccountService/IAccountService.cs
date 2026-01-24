using ECommerceApp.DTOs;
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

        Task<ApiResponse<LoginResponseDTO>> Login(
            LoginDTO loginDTO
        );

        Task<ApiResponse<EmployerResponseDTO>> RegisterCompany(CompanyRegistrationDTO registrationDTO);

    }

}
