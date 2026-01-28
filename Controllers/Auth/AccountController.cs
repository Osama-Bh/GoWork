using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.AuthDTOs;
using GoWork.DTOs.FileDTOs;
using GoWork.Models;
using GoWork.Service.AccountService;
using GoWork.Services.EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoWork.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration, IAccountService accountService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost("CandidateRegister")]
        public async Task<ActionResult<ApiResponse<CandidateResponseDTO>>> CandidateRegister(CandidateRegistrationDTO candidateRegistrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration data.");
            }

            var response = await _accountService.CandidateRegisterAsync(candidateRegistrationDTO);
            if (response.StatusCode != 201)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost("EmployerRegister")]
        public async Task<ActionResult<ApiResponse<EmployerResponseDTO>>> EmployerRegister(EmpolyerRegistrationDTO employerRegistrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid registration data.");
            }
            var response = await _accountService.RegisterCompany(employerRegistrationDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost("VerifyEmail")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> VerifyEmail(EmailConfirmationDTO confirmationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Confirmation data.");
            }

            var response = await _accountService.VerifyEmail(confirmationDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Login data.");
            }

            var response = await _accountService.Login(loginDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }


        //Added

        [HttpPost("CompanyLogin")]
        public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> LoginCompany(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Login data.");
            }

            var response = await _accountService.LoginCompany(loginDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }

            //var token = response.Data.Token;
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            var token = _accountService.GenerateJwtToken(user);

            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(response);
        }

        //Added

        [HttpPost("VerifyCompanyEmail")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> VerifyCompanyEmail(EmailConfirmationDTO confirmationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Confirmation data.");
            }

            var response = await _accountService.VerifyCompanyEmail(confirmationDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }

            // ✅ Generate JWT
            var user = await _userManager.FindByEmailAsync(confirmationDTO.Email);
            var token = _accountService.GenerateJwtToken(user);

            // ✅ Inject cookie
            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(response);
        }
        // Added

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return Ok("Logout Succcessfully");
        }

        [Authorize]
        [HttpPost("UploadResume")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UploadResume(UploadFileRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var response = await _accountService.UploadResume(requestDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPut("UpdateProfilePhoto")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdatePhoto(UpdateFileRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var response = await _accountService.UpdateFile(requestDTO, Enums.FileCategoryEnum.Image);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("DownloadProfilePhoto/{UserId}")]
        public async Task<ActionResult<ApiResponse<FileDownloadDto>>> GetProfileImage(int UserId)
        {
            if (UserId <= 0)
                return BadRequest("Invalid UserId.");

            var response = await _accountService.DownloadFile(UserId, Enums.FileCategoryEnum.Image);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPut("UpdateResume")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateResume(UpdateFileRequestDTO requestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var response = await _accountService.UpdateFile(requestDTO, Enums.FileCategoryEnum.Resume);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("DownloadResume/{UserId}")]
        public async Task<ActionResult<ApiResponse<FileDownloadDto>>> GetResume(int UserId)
        {
            if (UserId <= 0)
                return BadRequest("Invalid UserId.");

            var response = await _accountService.DownloadFile(UserId, Enums.FileCategoryEnum.Resume);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
