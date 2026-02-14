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
using System.Net;
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
        private readonly string _frontendBaseUrl;
        private readonly IWebHostEnvironment _env;
        public AccountController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration, IAccountService accountService, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _accountService = accountService;
            _frontendBaseUrl = configuration["Frontend:BaseUrl"];
            _env = env;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AdminTest")]
        public IActionResult GetAdmin()
        {
            return Ok("Welcome Admin");
        }

        [Authorize(Roles = "Company")]
        [HttpGet("CompanyTest")]
        public IActionResult GetCompany()
        {
            return Ok("Welcome Company");
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> Me()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                role = roles.FirstOrDefault()
            });
        }

        [HttpPost("Candidate/Register")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> CandidateRegister([FromForm]CandidateRegistrationDTO candidateRegistrationDTO)
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

        [HttpPost("Register")]
        public async Task<ActionResult<ApiResponse<EmployerResponseDTO>>> EmployerRegister([FromForm] EmpolyerRegistrationDTO employerRegistrationDTO)
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

        [HttpPost("Candidate/VerifyEmail")]
        public async Task<ActionResult<ApiResponse<CandidateResponseDTO2>>> VerifyEmail(EmailConfirmationDTO confirmationDTO)
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

        [HttpPost("Candidate/Login")]
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

        [HttpPost("Login")]
        public async Task<ActionResult<ApiResponse<EmployerResponseDTO>>> LoginCompany(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Login data.");
            }
            var clientType = Request.Headers["ClientType"].ToString();
            if (clientType == "web")
            {
                var response = await _accountService.LoginCompany(loginDTO);
                if (response.StatusCode != 200)
                {
                    return StatusCode((int)response.StatusCode, response);
                }

                //var token = response.Data.Token;
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);
                var token = _accountService.GenerateJwtToken(user);

                //Response.Cookies.Append("access_token", token, new CookieOptions
                //{
                //    HttpOnly = true,
                //    Secure = true,
                //    SameSite = SameSiteMode.None,
                //    Expires = DateTime.UtcNow.AddDays(7),
                //    Path = "/"
                //});

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7),
                    Path = "/"
                };

                if (!_env.IsDevelopment())
                {
                    cookieOptions.Domain = ".masarak.app";
                }

                Response.Cookies.Append("access_token", token, cookieOptions);


                return Ok(response);
            }
            else
            {
                var response = await _accountService.Login(loginDTO);
                if (response.StatusCode != 200)
                {
                    return StatusCode((int)response.StatusCode, response);
                }
                return Ok(response);


                //------


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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/"
            };

            if (!_env.IsDevelopment())
            {
                cookieOptions.Domain = ".masarak.app";
            }

            Response.Cookies.Append("access_token", token, cookieOptions);

            //Response.Cookies.Append("access_token", token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.None,
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    Path = "/",
            //    Domain = ".masarak.app"
            //});

            return Ok(response);
        }

        //Added

        [HttpPost("VerifyEmail")]
        public async Task<ActionResult<ApiResponse<EmployerResponseDTO>>> VerifyCompanyEmail(EmailConfirmationDTO confirmationDTO)
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

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/"
            };

            if (!_env.IsDevelopment())
            {
                cookieOptions.Domain = ".masarak.app";
            }

            Response.Cookies.Append("access_token", token, cookieOptions);

            //// ✅ Inject cookie
            //Response.Cookies.Append("access_token", token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.None,
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    Path = "/",
            //    Domain = ".masarak.app"
            //});

            return Ok(response);
        }

        // Added
        [Authorize]
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            //Response.Cookies.Delete("access_token", new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //    SameSite = SameSiteMode.None,
            //    Path = "/",
            //    Domain = ".masarak.app"
            //});

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = !_env.IsDevelopment(),
                SameSite = _env.IsDevelopment() ? SameSiteMode.Lax : SameSiteMode.None,
                Path = "/"
            };

            if (!_env.IsDevelopment())
            {
                cookieOptions.Domain = ".masarak.app";
            }

            Response.Cookies.Delete("access_token", cookieOptions);

            return Ok("Logout Successfully");
        }

        [Authorize]
        [HttpPatch("Candidate/UpdateProfile")]
        public async Task<ActionResult<ApiResponse<CandidateResponseDTO>>> UpdateCandidateProfile([FromForm]UpdateProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            // Retrieve the CandidateId from the authenticated user's claims
            var candidateIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (candidateIdClaim == null || !int.TryParse(candidateIdClaim.Value, out int candidateId))
            {
                return Unauthorized("Unauthorized: CandidateId not found.");
            }
            var response = await _accountService.UpdateCandidateProfileAsync(candidateId, profileDTO);

            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode,  response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("candidate/profile/me")]
        public async Task<ActionResult<ApiResponse<CandidateResponseDTO>>> GetCandidateProfile()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }
            // Retrieve the CandidateId from the authenticated user's claims
            var candidateIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (candidateIdClaim == null || !int.TryParse(candidateIdClaim.Value, out int candidateId))
            {
                return Unauthorized("Unauthorized: CandidateId not found.");
            }
            var response = await _accountService.GetCandidateProfileAsync(candidateId);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
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

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ForgotPassword(ForgetPasswordDTO forgetpasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Email");
            }
            var response = await _accountService.ForgetPassword(forgetpasswordDTO);
            
            if(response.StatusCode !=200)
            {
                return  StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> ResetPassword(ResetPasswordDTO resetpasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Email");
            }
            var response = await _accountService.ResetPassword(resetpasswordDTO);

            if(response.StatusCode !=200)
            {
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}
