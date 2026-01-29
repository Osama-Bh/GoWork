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

        Task<ApiResponse<ConfirmationResponseDTO>> ForgetPassword(
            ForgetPasswordDTO forgetpasswordDTO
        );

        Task<ApiResponse<ConfirmationResponseDTO>> ResetPassword(
            ResetPasswordDTO resetpasswordDTO
        );
        Task<ApiResponse<ConfirmationResponseDTO>> UpdateFile(UpdateFileRequestDTO requestDTO, FileCategoryEnum fileCategory);

        Task<ApiResponse<FileDownloadDto>> DownloadFile(int userId, FileCategoryEnum fileCategory);
        Task<ApiResponse<ConfirmationResponseDTO>> UploadResume(UploadFileRequestDTO requestDTO);



        // Token
        string GenerateJwtToken(ApplicationUser user);
    }

}
