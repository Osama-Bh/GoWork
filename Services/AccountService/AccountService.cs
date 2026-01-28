using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.AuthDTOs;
using GoWork.DTOs.FileDTOs;
using GoWork.Enums;
using GoWork.Models;
using GoWork.Services.EmailService;
using GoWork.Services.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GoWork.Service.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;

        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration, IFileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _fileService = fileService;
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> CandidateRegisterAsync(CandidateRegistrationDTO registrationDTO)
        {
            var categoryId = int.Parse(registrationDTO.InterstedInCategoryId);

            if (_context.TbCategories.FirstOrDefault(c => c.Id == categoryId) is null)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid category ID.");

            var user = new ApplicationUser
            {
                UserName = registrationDTO.Email,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registrationDTO.Password);

            if (!result.Succeeded)
                return new ApiResponse<ConfirmationResponseDTO>(400, "User creation failed! Please check user details and try again.");

            // 2️⃣ Handle skills
            var normalizedSkills = registrationDTO.ListOfSkills
                .Select(s => s.Trim().ToLower())
                .Distinct()
                .ToList();

            var existingSkills = _context.TbSkills
                .Where(s => normalizedSkills.Contains(s.Name.ToLower()))
                .ToList();

            var existingSkillNames = existingSkills
                .Select(s => s.Name.ToLower())
                .ToHashSet();

            var newSkills = normalizedSkills
                .Where(s => !existingSkillNames.Contains(s))
                .Select(s => new Skill { Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s) })
                .ToList();

            _context.TbSkills.AddRange(newSkills);
            var allSkills = existingSkills.Concat(newSkills).ToList();

            string profilePhotoUrl = null;
            if (registrationDTO.ProfilePhoto is not null)
            {
                var uploadResult = await _fileService.UploadAsync(registrationDTO.ProfilePhoto);
                if (uploadResult is not null)
                {
                    profilePhotoUrl = uploadResult.BlobUri;
                }
            }

            string resumeUrl = null;
            if (registrationDTO.Resume is not null)
            {
                var uploadResult = await _fileService.UploadAsync(registrationDTO.Resume);
                if (uploadResult is not null)
                {
                    resumeUrl = uploadResult.BlobUri;
                }
            }

            var seeker = new Seeker
            {
                FirsName = registrationDTO.FirstName,
                MiddleName = registrationDTO.MidName,
                LastName = registrationDTO.LastName,
                ProfilePhoto = profilePhotoUrl is null ? null : profilePhotoUrl,
                ResumeUrl = resumeUrl is null ? null : resumeUrl,
                SeekerSkills = allSkills.Select(skill => new SeekerSkill
                {
                    Skill = skill
                }).ToList(),
                InterestCategoryId = categoryId,
                UserId = user.Id
            };

            await _context.TbSeekers.AddAsync(seeker);
            await _context.SaveChangesAsync();

            //await transaction.CommitAsync();

            var confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

            await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"<p>Hello {registrationDTO.FirstName},</p> <p>Please use the code below to verify your email address:</p> <div style=\"font-size: 24px; font-weight: bold; letter-spacing: 4px; margin: 20px 0; text-align: center;\"> {confirmationToken} </div> <p>This code is valid for a limited time.</p> <p style=\"font-size: 12px; color: #777;\"> If you didn’t create a GoWork account, you can safely ignore this email. </p> <p>— GoWork Team</p> </div>", registrationDTO.FirstName);

            return new ApiResponse<ConfirmationResponseDTO>(201, new ConfirmationResponseDTO
            {
                Message = "There is a code have been sent to your email please check"
            });

        }

        //public async Task<ApiResponse<ConfirmationResponseDTO>> UploadFile(UploadFileRequestDTO fileRequestDTO, FileCategoryEnum fileCategory)
        //{
        //    var uploadResult = await _fileService.UploadAsync(fileRequestDTO.File);
        //    if (!uploadResult.Succeeded)
        //    {
        //        return new ApiResponse<ConfirmationResponseDTO>(400, "File upload failed.");
        //    }

        //    var candidate = await _context.TbSeekers.FirstOrDefaultAsync(c => c.Id == fileRequestDTO.UserId);
        //    if (candidate == null)
        //    {
        //        return new ApiResponse<ConfirmationResponseDTO>(404, "Candidate not found.");
        //    }
            
        //    if (fileCategory == FileCategoryEnum.Image)
        //        candidate.ProfilePhoto = uploadResult.BlobUri;
        //    else if (fileCategory == FileCategoryEnum.Resume)
        //        candidate.ResumeUrl = uploadResult.BlobUri;

        //    await _context.SaveChangesAsync();

        //    return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
        //    {
        //        Message = "File uploaded successfully."
        //    });
        //}

        public async Task<ApiResponse<ConfirmationResponseDTO>> RegisterCompany(EmpolyerRegistrationDTO registrationDTO)
        {

            var user = new ApplicationUser
            {
                UserName = registrationDTO.Email,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registrationDTO.Password);

            if (!result.Succeeded)
                return new ApiResponse<ConfirmationResponseDTO>(400, "User creation failed! Please check user details and try again.");

            var employer = new Employer
            {
                UserId = user.Id,
                ComapnyName = registrationDTO.CompanyName,
                Industry = registrationDTO.Industry,
                LogoUrl = registrationDTO.LogoUrl,
                EmployerStatusId = (int)EmployerStatusEnum.PendingApproval,
            };


            await _context.TbEmployers.AddAsync(employer);
            await _context.SaveChangesAsync();

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"<p>Hello {registrationDTO.CompanyName},</p> <p>Please use the code below to verify your email address:</p> <div style=\"font-size: 24px; font-weight: bold; letter-spacing: 4px; margin: 20px 0; text-align: center;\"> {confirmationToken} </div> <p>This code is valid for a limited time.</p> <p style=\"font-size: 12px; color: #777;\"> If you didn’t create a GoWork account, you can safely ignore this email. </p> <p>— GoWork Team</p> </div>", registrationDTO.CompanyName);
            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "There is a code have been sent to your email please check"
            });
        }
        public async Task<ApiResponse<CandidateResponseDTO>> VerifyEmail(EmailConfirmationDTO confirmationDTO)
        {
            if (string.IsNullOrEmpty(confirmationDTO.Email) || string.IsNullOrEmpty(confirmationDTO.EmailConfirmationCode))
                return new ApiResponse<CandidateResponseDTO>(400, "Email or Confimation Code is missing !");

            var user = await _userManager.FindByEmailAsync(confirmationDTO.Email);

            if (user is null)
            {
                return new ApiResponse<CandidateResponseDTO>(400, "User Nou Found.");
            }

            var isVerfied = await _userManager.ConfirmEmailAsync(user, confirmationDTO.EmailConfirmationCode);

            if (isVerfied.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Candidate");
                var candidate = _context.TbSeekers.FirstOrDefault(c => c.UserId == user.Id);

                if (candidate is null)
                    return new ApiResponse<CandidateResponseDTO>(400, "Candidate profile not found.");

                if (candidate.ProfilePhoto is null)
                {
                    return new ApiResponse<CandidateResponseDTO>(200, new CandidateResponseDTO
                    {
                        CandidateId = candidate.Id,
                        Email = user.Email,
                        Role = "Candidate",
                        Token = GenerateJwtToken(user)
                    });
                }

                var downLoadResult = _fileService.DownloadUrlAsync(candidate.ProfilePhoto);

                return new ApiResponse<CandidateResponseDTO>(200, new CandidateResponseDTO
                {
                    CandidateId = candidate.Id,
                    Email = user.Email,
                    SasUrl = downLoadResult.SasUrl,
                    ExpiresAt = downLoadResult.ExpiresAt,
                    Role = "Candidate",
                    Token = GenerateJwtToken(user)
                });
                
            }
            else
            {
                return new ApiResponse<CandidateResponseDTO>(400, "Email verification failed. Please check the confirmation code and try again.");
            }
        }

        public async Task<ApiResponse<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return new ApiResponse<LoginResponseDTO>(404, "User not found.");

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return new ApiResponse<LoginResponseDTO>(400, "Invalid password.");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new ApiResponse<LoginResponseDTO>(401, "Email is not confirmed. Please verify your email before logging in.");

            var token = GenerateJwtToken(user);
            return new ApiResponse<LoginResponseDTO>(200, new LoginResponseDTO { Token = token });
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // Add user roles
            var roles = _userManager.GetRolesAsync(user).Result;
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Added 
        public async Task<ApiResponse<EmployerResponseDTO>> LoginCompany(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return new ApiResponse<EmployerResponseDTO>(404, "User not found.");

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return new ApiResponse<EmployerResponseDTO>(400, "Invalid password.");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new ApiResponse<EmployerResponseDTO>(401, "Email is not confirmed. Please verify your email before logging in.");

            //var token = GenerateJwtToken(user);

            var company = _context.TbEmployers.FirstOrDefault(c => c.UserId == user.Id);

            return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
            {
                EmployerId = company.Id,
                Email = user.Email,
                CompanyName = company.ComapnyName
            });
        }


        //Added
        public async Task<ApiResponse<EmployerResponseDTO>> VerifyCompanyEmail(EmailConfirmationDTO confirmationDTO)
        {
            if (string.IsNullOrEmpty(confirmationDTO.Email) || string.IsNullOrEmpty(confirmationDTO.EmailConfirmationCode))
                return new ApiResponse<EmployerResponseDTO>(400, "Email or Confimation Code is missing !");

            var user = await _userManager.FindByEmailAsync(confirmationDTO.Email);

            if (user is null)
            {
                return new ApiResponse<EmployerResponseDTO>(400, "User Nou Found.");
            }

            var isVerfied = await _userManager.ConfirmEmailAsync(user, confirmationDTO.EmailConfirmationCode);

            if (isVerfied.Succeeded)
            {
                
                var company = _context.TbEmployers.FirstOrDefault(c=> c.UserId == user.Id);

                return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
                {
                    EmployerId = company.Id,
                    Email = user.Email,
                    CompanyName = company.ComapnyName
                });
            }
            else
            {
                return new ApiResponse<EmployerResponseDTO>(400, "Email verification failed. Please check the confirmation code and try again.");
            }
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UploadResume(UploadFileRequestDTO requestDTO)
        {
            try
            {
                var candidate = await _context.TbSeekers.FirstOrDefaultAsync(c => c.Id == requestDTO.UserId);

                if (candidate is null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Candidate not found.");


                var uploadResult = await _fileService.UploadAsync(requestDTO.File);
                if (uploadResult is null)
                {
                    return new ApiResponse<ConfirmationResponseDTO>(400, "File upload failed.");
                }

                candidate.ResumeUrl = uploadResult.BlobUri;
                await _context.SaveChangesAsync();
                return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
                {
                    Message = "File uploaded successfully."
                });
            }
            catch (Exception)
            {
                return new ApiResponse<ConfirmationResponseDTO>(500, "An error occurred while uploading the file.");
            }
        }
        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateFile(UpdateFileRequestDTO requestDTO, FileCategoryEnum fileCategory)
        {
            try
            {
                bool isUpdated = false;
                var candidate = await _context.TbSeekers.FirstOrDefaultAsync(c => c.Id == requestDTO.UserId);

                if (candidate is null)
                    return new ApiResponse<ConfirmationResponseDTO>(404, "Candidate not found.");

                if (fileCategory == FileCategoryEnum.Resume)
                    isUpdated = await _fileService.UpdateAsync(requestDTO.File, candidate.ResumeUrl);
                else if (fileCategory == FileCategoryEnum.Image)
                    isUpdated = await _fileService.UpdateAsync(requestDTO.File, candidate.ProfilePhoto);

                return isUpdated
                    ? new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO { Message = "File updated successfully." })
                    : new ApiResponse<ConfirmationResponseDTO>(400, "File update failed.");

            }
            catch (Exception)
            {
                return new ApiResponse<ConfirmationResponseDTO>(500, "An error occurred while updating the file.");
            }
        }

        public async Task<ApiResponse<FileDownloadDto>> DownloadFile(int userId, FileCategoryEnum fileCategory)
        {
            try
            {
                var candidate = await _context.TbSeekers.FirstOrDefaultAsync(c => c.Id == userId);

                if (candidate is null)
                    return new ApiResponse<FileDownloadDto>(404, "Candidate not found.");

                FileDownloadDto response = new();

                if (fileCategory == FileCategoryEnum.Resume)
                {
                    if (string.IsNullOrEmpty(candidate.ResumeUrl))
                        return new ApiResponse<FileDownloadDto>(404, "Resume not found.");
                    response = _fileService.DownloadUrlAsync(candidate.ResumeUrl);
                }
                else if (fileCategory == FileCategoryEnum.Image)
                {
                    if (string.IsNullOrEmpty(candidate.ProfilePhoto))
                        return new ApiResponse<FileDownloadDto>(404, "Profile photo not found.");
                    response = _fileService.DownloadUrlAsync(candidate.ProfilePhoto);
                }

                return new ApiResponse<FileDownloadDto>(200, response);

            }
            catch (Exception)
            {
                return new ApiResponse<FileDownloadDto>(500, "An error occurred while generating the download URL.");
            }
        }
    }
}
