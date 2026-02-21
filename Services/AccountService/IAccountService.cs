using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.AuthDTOs;
using GoWork.DTOs.FileDTOs;
using GoWork.Enums;

namespace GoWork.Service.AccountService
{
    public interface IAccountService
    {
        // Candidate
        Task<ApiResponse<ConfirmationResponseDTO>> CandidateRegisterAsync(
            CandidateRegistrationDTO registrationDTO
        );

        //Task<ApiResponse<ConfirmationResponseDTO>> UploadFile(UploadFileRequestDTO fileRequestDTO, FileCategoryEnum fileCategory);


        Task<ApiResponse<CandidateResponseDTO2>> VerifyEmail(
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

        Task<ApiResponse<ConfirmationResponseDTO>> ForgetPassword(
            ForgetPasswordDTO forgetpasswordDTO
        );

        Task<ApiResponse<ConfirmationResponseDTO>> ResetPassword(
            ResetPasswordDTO resetpasswordDTO
        );

        Task<ApiResponse<CandidateResponseDTO>> UpdateCandidateProfileAsync(int candidateId, UpdateProfileDTO dto);
        Task<ApiResponse<CandidateResponseDTO>> GetCandidateProfileAsync(int userId);

        Task<ApiResponse<ConfirmationResponseDTO>> UpdateFile(UpdateFileRequestDTO requestDTO, FileCategoryEnum fileCategory);

        Task<ApiResponse<FileDownloadDto>> DownloadFile(int userId, FileCategoryEnum fileCategory);
        Task<ApiResponse<ConfirmationResponseDTO>> UploadResume(UploadFileRequestDTO requestDTO);
        Task<ApiResponse<ConfirmationResponseDTO>> DeleteAccountAsync(int userId);
        Task<ApiResponse<ConfirmationResponseDTO>> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCompanyProfileAsync(int userId, UpdateCompanyProfileDTO dto);




        // Token
        string GenerateJwtToken(ApplicationUser user);
    }

}
