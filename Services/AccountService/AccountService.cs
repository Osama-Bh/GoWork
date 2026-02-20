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
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly string _frontendBaseUrl;
        public AccountService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService, IConfiguration configuration, IFileService fileService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _fileService = fileService;
            _frontendBaseUrl = configuration["Frontend:BaseUrl"];
        }

        //public async Task<ApiResponse<ConfirmationResponseDTO>> CandidateRegisterAsync(CandidateRegistrationDTO registrationDTO)
        //{
        //    await using var transaction = await _context.Database.BeginTransactionAsync();

        //    ApplicationUser user = null;
        //    try
        //    {
        //        var categoryId = int.Parse(registrationDTO.InterstedInCategoryId);

        //        if (_context.TbCategories.FirstOrDefault(c => c.Id == categoryId) is null)
        //            return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid category ID.");

        //        user = new ApplicationUser
        //        {
        //            UserName = registrationDTO.Email,
        //            Email = registrationDTO.Email,
        //            PhoneNumber = registrationDTO.PhoneNumber,
        //        };

        //        var result = await _userManager.CreateAsync(user, registrationDTO.Password);

        //        if (!result.Succeeded)
        //            return new ApiResponse<ConfirmationResponseDTO>(400, "User creation failed! Please check user details and try again.");

        //        // 2️⃣ Handle skills
        //        var normalizedSkills = registrationDTO.ListOfSkills
        //            .Select(s => s.Trim().ToLower())
        //            .Distinct()
        //            .ToList();

        //        var existingSkills = _context.TbSkills
        //            .Where(s => normalizedSkills.Contains(s.Name.ToLower()))
        //            .ToList();

        //        var existingSkillNames = existingSkills
        //            .Select(s => s.Name.ToLower())
        //            .ToHashSet();

        //        var newSkills = normalizedSkills
        //            .Where(s => !existingSkillNames.Contains(s))
        //            .Select(s => new Skill { Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s) })
        //            .ToList();

        //        _context.TbSkills.AddRange(newSkills);
        //        var allSkills = existingSkills.Concat(newSkills).ToList();

        //        string profilePhotoUrl = null;
        //        if (registrationDTO.ProfilePhoto is not null)
        //        {
        //            var uploadResult = await _fileService.UploadAsync(registrationDTO.ProfilePhoto);
        //            if (uploadResult is not null)
        //            {
        //                profilePhotoUrl = uploadResult.BlobUri;
        //            }
        //        }

        //        string resumeUrl = null;
        //        if (registrationDTO.Resume is not null)
        //        {
        //            var uploadResult = await _fileService.UploadAsync(registrationDTO.Resume);
        //            if (uploadResult is not null)
        //            {
        //                resumeUrl = uploadResult.BlobUri;
        //            }
        //        }

        //        var seeker = new Seeker
        //        {
        //            FirsName = registrationDTO.FirstName,
        //            MiddleName = registrationDTO.MidName,
        //            LastName = registrationDTO.LastName,
        //            ProfilePhoto = profilePhotoUrl is null ? null : profilePhotoUrl,
        //            ResumeUrl = resumeUrl is null ? null : resumeUrl,
        //            SeekerSkills = allSkills.Select(skill => new SeekerSkill
        //            {
        //                Skill = skill
        //            }).ToList(),
        //            InterestCategoryId = categoryId,
        //            UserId = user.Id
        //        };

        //        await _context.TbSeekers.AddAsync(seeker);
        //        await _context.SaveChangesAsync();

        //        await transaction.CommitAsync();

        //        var confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

        //        await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"<p>Hello {registrationDTO.FirstName},</p> <p>Please use the code below to verify your email address:</p> <div style=\"font-size: 24px; font-weight: bold; letter-spacing: 4px; margin: 20px 0; text-align: center;\"> {confirmationToken} </div> <p>This code is valid for a limited time.</p> <p style=\"font-size: 12px; color: #777;\"> If you didn’t create a GoWork account, you can safely ignore this email. </p> <p>— GoWork Team</p> </div>", registrationDTO.FirstName);

        //        return new ApiResponse<ConfirmationResponseDTO>(201, new ConfirmationResponseDTO
        //        {
        //            Message = "There is a code have been sent to your email please check"
        //        });
        //    }
        //    catch (Exception)
        //    {
        //        await transaction.RollbackAsync();

        //        // 🔥 IMPORTANT: cleanup Identity user manually
        //        if (user != null)
        //        {
        //            await _userManager.DeleteAsync(user);
        //        }

        //        // Log ex here (ILogger)

        //        return new ApiResponse<ConfirmationResponseDTO>(
        //            500,
        //            "Registration failed. Please try again."
        //        );
        //    }


        //}

        private static string BuildArabicTemplate(string title, string bodyContent)
        {
            return $@"
            <!DOCTYPE html>
            <html lang='ar'>
            <head>
              <meta charset='UTF-8'>
            </head>
            <body style='font-family: Arial, sans-serif; background-color:#f4f6f8; padding:20px; direction:rtl;'>
              <div style='max-width:600px; margin:auto; background:#ffffff; padding:30px; border-radius:8px; text-align:right;'>

                <h2 style='color:#333;'>{title}</h2>

                {bodyContent}

                <p style='color:#aaa; font-size:12px;text-align:left; direction:ltr;'>
                  © Masarak.
                </p>

              </div>
            </body>
            </html>";
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> CandidateRegisterAsync(CandidateRegistrationDTO registrationDTO)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            ApplicationUser user = null;
            try
            {
                var categoryId = int.Parse(registrationDTO.InterstedInCategoryId);

                if (_context.TbCategories.FirstOrDefault(c => c.Id == categoryId) is null)
                    return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid category ID.");

                user = new ApplicationUser
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

                await transaction.CommitAsync();

                var confirmationToken = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                var content = $@"
                            <p style='color:#555;'>
                                مرحبًا {registrationDTO.FirstName},<br/>
                                من فضلك استخدم الرمز أدناه لتأكيد بريدك الإلكتروني.
                            </p>

                            <div style='text-align:center; margin:30px 0;'>
                                <span style='display:inline-block; 
                                            background-color:#2563eb; 
                                            color:#ffffff; 
                                            font-size:24px; 
                                            font-weight:bold; 
                                            padding:15px 25px; 
                                            border-radius:6px; 
                                            letter-spacing:4px;'>
                                {confirmationToken}
                                </span>
                            </div>

                            <p style='color:#888; font-size:14px;'>
                                هذا الرمز صالح لفترة محدودة.
                            </p>

                            <p style='color:#888; font-size:14px;'>
                                إذا لم تقم بإنشاء حساب في Masarak، يمكنك تجاهل هذه الرسالة بأمان.
                            </p>";

                var htmlBody = BuildArabicTemplate("تأكيد البريد الإلكتروني", content);

                await _emailService.SendEmailAsync(user.Email, "Verify Your Email", htmlBody, registrationDTO.FirstName);

                return new ApiResponse<ConfirmationResponseDTO>(201, new ConfirmationResponseDTO
                {
                    Message = "There is a code have been sent to your email please check"
                });
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();

                // 🔥 IMPORTANT: cleanup Identity user manually
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }

                // Log ex here (ILogger)

                return new ApiResponse<ConfirmationResponseDTO>(
                    500,
                    "Registration failed. Please try again."
                );
            }


        }

        public async Task<ApiResponse<CandidateResponseDTO>> UpdateCandidateProfileAsync(int userId, UpdateProfileDTO dto)
        {
            try
            {
                var candidate = await _context.TbSeekers
                .Include(c => c.SeekerSkills)
                    .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.UserId == userId);

                if (candidate == null)
                    throw new Exception("Candidate not found");

                // =========================
                // Scalar fields (PATCH)
                // =========================
                if (dto.FirstName != null)
                    candidate.FirsName = dto.FirstName;

                if (dto.MiddleName != null)
                    candidate.MiddleName = dto.MiddleName;

                if (dto.LastName != null)
                    candidate.LastName = dto.LastName;

                // =========================
                // Profile photo upload
                // =========================
                if (dto.ProfilePhoto != null)
                {
                    if (candidate.ProfilePhoto is not null)
                    {
                        var isUpdated = await _fileService.UpdateAsync(
                            dto.ProfilePhoto,
                            candidate.ProfilePhoto);
                    }
                    else
                    {
                        var profilePhotoUrl = await _fileService.UploadAsync(dto.ProfilePhoto);
                        candidate.ProfilePhoto = profilePhotoUrl?.BlobUri;
                    }
                }

                // =========================
                // Resume upload
                // =========================
                if (dto.ResumeFile != null)
                {
                    if (candidate.ResumeUrl is not null)
                    {
                        var isUpdated = await _fileService.UpdateAsync(
                            dto.ResumeFile,
                            candidate.ResumeUrl);
                    }
                    else
                    {
                        var resumeUrl = await _fileService.UploadAsync(dto.ResumeFile);
                        candidate.ResumeUrl = resumeUrl?.BlobUri;
                    }
                }

                // =========================
                // Skills (Many-to-Many)
                // =========================
                if (dto.Skills != null)
                {
                    // Normalize skill names
                    var normalizedSkillNames = dto.Skills
                        .Select(s => s.Trim().ToLower())
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Distinct()
                        .ToList();

                    // Get skills that already exist
                    var existingSkills = await _context.TbSkills
                        .Where(s => normalizedSkillNames.Contains(s.Name.ToLower()))
                        .ToListAsync();

                    // Detect new skills
                    var newSkillNames = normalizedSkillNames
                        .Except(existingSkills.Select(s => s.Name.ToLower()))
                        .ToList();

                    // Insert new skills
                    foreach (var skillName in newSkillNames)
                    {
                        var skill = new Skill
                        {
                            Name = skillName
                        };

                        _context.TbSkills.Add(skill);
                        existingSkills.Add(skill);
                    }

                    // Replace candidate skills (links only)
                    _context.TbSeekerSkills.RemoveRange(candidate.SeekerSkills);
                    candidate.SeekerSkills.Clear();

                    // Add new relations one by one
                    foreach (var skill in existingSkills)
                    {
                        candidate.SeekerSkills.Add(new SeekerSkill
                        {
                            SeekerId = candidate.Id,
                            Skill = skill
                        });
                    }
                }

                await _context.SaveChangesAsync();

                List<FileDownloadDto> lstFillsDTO = new List<FileDownloadDto>();
                if (candidate.ProfilePhoto is not null)
                    lstFillsDTO.Add(_fileService.DownloadUrlAsync(candidate.ProfilePhoto));

                if (candidate.ResumeUrl is not null)
                    lstFillsDTO.Add(_fileService.DownloadUrlAsync(candidate.ResumeUrl));

                // =========================
                // Return updated profile
                // =========================
                return new ApiResponse<CandidateResponseDTO>(200, new CandidateResponseDTO
                {
                    FirstName = candidate.FirsName,
                    MiddleName = candidate.MiddleName,
                    LastName = candidate.LastName,
                    ProfilPhotoUrl = lstFillsDTO[0] is null ? null : lstFillsDTO[0].SasUrl,
                    ResumeUrl = lstFillsDTO[1] is null ? null : lstFillsDTO[1].SasUrl,
                    Skills = candidate.SeekerSkills?
                    .Where(cs => cs.Skill != null)
                    .Select(cs => cs.Skill!.Name)
                    .ToList() ?? new List<string>()
                });
            }
            catch (Exception)
            {
                return new ApiResponse<CandidateResponseDTO>(500, "An error occurred while updating the profile.");
            }
            
        }

        public async Task<ApiResponse<CandidateResponseDTO>> GetCandidateProfileAsync(int userId)
        {
            var candidate = await _context.TbSeekers
                .Include(c => c.SeekerSkills)
                    .ThenInclude(cs => cs.Skill)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (candidate == null)
                return new ApiResponse<CandidateResponseDTO>(404, "Candidate not found.");

            var user = await _userManager.FindByIdAsync(candidate.UserId.ToString());

            List<FileDownloadDto> lstFillsDTO = new List<FileDownloadDto>();
            if (candidate.ProfilePhoto is not null)
                lstFillsDTO.Add(_fileService.DownloadUrlAsync(candidate.ProfilePhoto));

            if (candidate.ResumeUrl is not null)
                lstFillsDTO.Add(_fileService.DownloadUrlAsync(candidate.ResumeUrl));

            return new ApiResponse<CandidateResponseDTO>(200, new CandidateResponseDTO
            {
                FirstName = candidate.FirsName,
                MiddleName = string.IsNullOrEmpty(candidate.MiddleName) ? null : candidate.MiddleName,
                LastName = candidate.LastName,
                ProfilPhotoUrl = lstFillsDTO[0] is null ? null : lstFillsDTO[0].SasUrl,
                ResumeUrl = lstFillsDTO[1] is null ? null : lstFillsDTO[1].SasUrl,
                Skills = candidate.SeekerSkills?
                    .Where(cs => cs.Skill != null)
                    .Select(cs => cs.Skill!.Name)
                    .ToList() ?? new List<string>()
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

        //public async Task<ApiResponse<ConfirmationResponseDTO>> RegisterCompany(EmpolyerRegistrationDTO registrationDTO)
        //{

        //    var user = new ApplicationUser
        //    {
        //        UserName = registrationDTO.Email,
        //        Email = registrationDTO.Email,
        //        PhoneNumber = registrationDTO.PhoneNumber,
        //    };

        //    var result = await _userManager.CreateAsync(user, registrationDTO.Password);

        //    if (!result.Succeeded)
        //        return new ApiResponse<ConfirmationResponseDTO>(400, "User creation failed! Please check user details and try again.");

        //    string logoUrl = null;
        //    if (registrationDTO.LogoUrl is not null)
        //    {
        //        var uploadResult = await _fileService.UploadAsync(registrationDTO.LogoUrl);
        //        if (uploadResult is not null)
        //        {
        //            logoUrl = uploadResult.BlobUri;
        //        }
        //    }

        //    var employer = new Employer
        //    {
        //        UserId = user.Id,
        //        ComapnyName = registrationDTO.CompanyName,
        //        Industry = registrationDTO.Industry,
        //        LogoUrl = logoUrl,
        //        EmployerStatusId = (int)EmployerStatusEnum.PendingApproval,
        //    };


        //    await _context.TbEmployers.AddAsync(employer);
        //    await _context.SaveChangesAsync();

        //    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        //    await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"<p>Hello {registrationDTO.CompanyName},</p> <p>Please use the code below to verify your email address:</p> <div style=\"font-size: 24px; font-weight: bold; letter-spacing: 4px; margin: 20px 0; text-align: center;\"> {confirmationToken} </div> <p>This code is valid for a limited time.</p> <p style=\"font-size: 12px; color: #777;\"> If you didn’t create a GoWork account, you can safely ignore this email. </p> <p>— GoWork Team</p> </div>", registrationDTO.CompanyName);
        //    return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
        //    {
        //        Message = "There is a code have been sent to your email please check"
        //    });
        //}

        public async Task<ApiResponse<ConfirmationResponseDTO>> RegisterCompany(EmpolyerRegistrationDTO registrationDTO)
        {
            var UserFound = await _userManager.FindByEmailAsync(registrationDTO.Email);

            if (UserFound is not null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(400, "البريد الإلكتروني مستخدم بالفعل");
            }

            var user = new ApplicationUser
            {
                UserName = registrationDTO.Email,
                Email = registrationDTO.Email,
                PhoneNumber = registrationDTO.PhoneNumber,
            };


            


            var result = await _userManager.CreateAsync(user, registrationDTO.Password);

            if (!result.Succeeded)
                return new ApiResponse<ConfirmationResponseDTO>(400, "حدث مشكلة في إنشاء الحساب،حاول مرة أخرى");

            string logoUrl = null;
            if (registrationDTO.LogoUrl is not null)
            {
                var uploadResult = await _fileService.UploadAsync(registrationDTO.LogoUrl);
                if (uploadResult is not null)
                {
                    logoUrl = uploadResult.BlobUri;
                }
            }

            var employer = new Employer
            {
                UserId = user.Id,
                ComapnyName = registrationDTO.CompanyName,
                Industry = registrationDTO.Industry,
                LogoUrl = logoUrl,
                EmployerStatusId = (int)EmployerStatusEnum.PendingApproval,
            };


            await _context.TbEmployers.AddAsync(employer);
            await _context.SaveChangesAsync();

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var content = $@"
                    <p style='color:#555;'>
                      مرحبًا {registrationDTO.CompanyName},<br/>
                      من فضلك استخدم الرمز أدناه لتأكيد بريدك الإلكتروني.
                    </p>

                    <div style='text-align:center; margin:30px 0;'>
                      <span style='display:inline-block; 
                                   background-color:#2563eb; 
                                   color:#ffffff; 
                                   font-size:24px; 
                                   font-weight:bold; 
                                   padding:15px 25px; 
                                   border-radius:6px; 
                                   letter-spacing:4px;'>
                        {confirmationToken}
                      </span>
                    </div>

                    <p style='color:#888; font-size:14px;'>
                      هذا الرمز صالح لفترة محدودة.
                    </p>

                    <p style='color:#888; font-size:14px;'>
                      إذا لم تقم بإنشاء حساب في Masarak، يمكنك تجاهل هذه الرسالة بأمان.
                    </p>";

            var htmlBody = BuildArabicTemplate("تأكيد البريد الإلكتروني", content);


            await _emailService.SendEmailAsync(user.Email, "Verify Your Email", htmlBody, registrationDTO.CompanyName);
            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "There is a code have been sent to your email please check"
            });
        }
        public async Task<ApiResponse<CandidateResponseDTO2>> VerifyEmail(EmailConfirmationDTO confirmationDTO)
        {
            if (string.IsNullOrEmpty(confirmationDTO.Email) || string.IsNullOrEmpty(confirmationDTO.EmailConfirmationCode))
                return new ApiResponse<CandidateResponseDTO2>(400, "Email or Confimation Code is missing !");

            var user = await _userManager.FindByEmailAsync(confirmationDTO.Email);

            if (user is null)
            {
                return new ApiResponse<CandidateResponseDTO2>(400, "User Nou Found.");
            }

            var isVerfied = await _userManager.ConfirmEmailAsync(user, confirmationDTO.EmailConfirmationCode);

            if (isVerfied.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Candidate");
                var candidate = _context.TbSeekers.FirstOrDefault(c => c.UserId == user.Id);

                if (candidate is null)
                    return new ApiResponse<CandidateResponseDTO2>(400, "Candidate profile not found.");

                if (candidate.ProfilePhoto is null)
                {
                    return new ApiResponse<CandidateResponseDTO2>(200, new CandidateResponseDTO2
                    {
                        CandidateId = candidate.Id,
                        Email = user.Email,
                        Role = "Candidate",
                        Token = GenerateJwtToken(user)
                    });
                }

                var downLoadResult = _fileService.DownloadUrlAsync(candidate.ProfilePhoto);

                return new ApiResponse<CandidateResponseDTO2>(200, new CandidateResponseDTO2
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
                return new ApiResponse<CandidateResponseDTO2>(400, "Email verification failed. Please check the confirmation code and try again.");
            }
        }

        public async Task<ApiResponse<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return new ApiResponse<LoginResponseDTO>(404, "User not found.");

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return new ApiResponse<LoginResponseDTO>(400, "Invalid password.");

            //if (!await _userManager.IsEmailConfirmedAsync(user))
            //    return new ApiResponse<LoginResponseDTO>(401, "Email is not confirmed. Please verify your email before logging in.");

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                

                await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"<p>Hello {user?.UserName},</p> <p>Please use the code below to verify your email address:</p> <div style=\"font-size: 24px; font-weight: bold; letter-spacing: 4px; margin: 20px 0; text-align: center;\"> {confirmationToken} </div> <p>This code is valid for a limited time.</p> <p style=\"font-size: 12px; color: #777;\"> If you didn’t create a GoWork account, you can safely ignore this email. </p> <p>— GoWork Team</p> </div>", user.UserName);
                //return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
                //{
                //    Message = "There is a code have been sent to your email please check"
                //});
                return new ApiResponse<LoginResponseDTO>(401, "Email is not confirmed. Please verify your email before logging in.");
            }

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
        //public async Task<ApiResponse<EmployerResponseDTO>> LoginCompany(LoginDTO loginDTO)
        //{
        //    var user = await _userManager.FindByEmailAsync(loginDTO.Email);

        //    if (user is null)
        //        return new ApiResponse<EmployerResponseDTO>(404, "User not found.");

        //    if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
        //        return new ApiResponse<EmployerResponseDTO>(400, "Invalid password.");

        //    if (!await _userManager.IsEmailConfirmedAsync(user))
        //        return new ApiResponse<EmployerResponseDTO>(401, "Email is not confirmed. Please verify your email before logging in.");

        //    var roles = await _userManager.GetRolesAsync(user);
        //    var role = roles.FirstOrDefault() ?? "Unknown";

        //    //var token = GenerateJwtToken(user);

        //    if(role == "Company")
        //    {
        //        var company = _context.TbEmployers.FirstOrDefault(c => c.UserId == user.Id);

        //        if (company == null)
        //            return new ApiResponse<EmployerResponseDTO>(404, "Employer info not found for this user.");

        //        var downLoadResult = _fileService.DownloadUrlAsync(company.LogoUrl);



        //        return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
        //        {
        //            EmployerId = company.Id,
        //            Email = user.Email,
        //            SasUrl = downLoadResult.SasUrl,
        //            ExpiresAt = downLoadResult.ExpiresAt,
        //            //Role = "Company",
        //            Role = role,
        //            CompanyName = company.ComapnyName
        //        });
        //    }
        //    else
        //    {
        //        // For other roles, return minimal info without company details
        //        return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
        //        {
        //            EmployerId = user.Id, // or null if your DTO supports it
        //            Email = user.Email,
        //            Role = role
        //        });
        //    }

        //}

        public async Task<ApiResponse<EmployerResponseDTO>> LoginCompany(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);

            if (user is null)
                return new ApiResponse<EmployerResponseDTO>(400, "البريد الإلكتروني أو كلمة المرور غير صحيحة");

            if (!await _userManager.CheckPasswordAsync(user, loginDTO.Password))
                return new ApiResponse<EmployerResponseDTO>(400, "البريد الإلكتروني أو كلمة المرور غير صحيحة");

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var company = _context.TbEmployers.FirstOrDefault(c => c.UserId == user.Id);

                var content = $@"
                    <p style='color:#555;'>
                      مرحبًا {company.ComapnyName},<br/>
                      من فضلك استخدم الرمز أدناه لتأكيد بريدك الإلكتروني.
                    </p>

                    <div style='text-align:center; margin:30px 0;'>
                      <span style='display:inline-block; 
                                   background-color:#2563eb; 
                                   color:#ffffff; 
                                   font-size:24px; 
                                   font-weight:bold; 
                                   padding:15px 25px; 
                                   border-radius:6px; 
                                   letter-spacing:4px;'>
                        {confirmationToken}
                      </span>
                    </div>

                    <p style='color:#888; font-size:14px;'>
                      هذا الرمز صالح لفترة محدودة.
                    </p>

                    <p style='color:#888; font-size:14px;'>
                      إذا لم تقم بإنشاء حساب في Masarak، يمكنك تجاهل هذه الرسالة بأمان.
                    </p>";

                var htmlBody = BuildArabicTemplate("تأكيد البريد الإلكتروني", content);


                await _emailService.SendEmailAsync(user.Email, "Verify Your Email", htmlBody, company.ComapnyName);

                //await _emailService.SendEmailAsync(user.Email, "Verify Your Email", $"<p>Hello {company?.ComapnyName},</p> <p>Please use the code below to verify your email address:</p> <div style=\"font-size: 24px; font-weight: bold; letter-spacing: 4px; margin: 20px 0; text-align: center;\"> {confirmationToken} </div> <p>This code is valid for a limited time.</p> <p style=\"font-size: 12px; color: #777;\"> If you didn’t create a GoWork account, you can safely ignore this email. </p> <p>— GoWork Team</p> </div>", company.ComapnyName);
                //return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
                //{
                //    Message = "There is a code have been sent to your email please check"
                //});
                return new ApiResponse<EmployerResponseDTO>(401, "Email is not confirmed. Please verify your email before logging in.");
            }


            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Unknown";

            //var token = GenerateJwtToken(user);

            if (role == "Company")
            {
                var company = _context.TbEmployers.FirstOrDefault(c => c.UserId == user.Id);

                if (company == null)
                    return new ApiResponse<EmployerResponseDTO>(404, "Employer info not found for this user.");

                var downLoadResult = _fileService.DownloadUrlAsync(company.LogoUrl);



                return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
                {
                    EmployerId = company.Id,
                    Email = user.Email,
                    SasUrl = downLoadResult.SasUrl,
                    ExpiresAt = downLoadResult.ExpiresAt,
                    //Role = "Company",
                    Role = role,
                    CompanyName = company.ComapnyName
                });
            }
            else
            {
                // For other roles, return minimal info without company details
                return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
                {
                    EmployerId = user.Id, // or null if your DTO supports it
                    Email = user.Email,
                    Role = role
                });
            }

        }

        //public async Task<ApiResponse<ConfirmationResponseDTO>> ForgetPassword(ForgetPasswordDTO forgetpasswordDTO)
        //{

        //    var user = await _userManager.FindByEmailAsync(forgetpasswordDTO.Email);

        //    if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        //        return new ApiResponse<ConfirmationResponseDTO>(400,"Invalid request"); // Prevent account enumeration

        //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //    //var encodedToken = WebUtility.UrlEncode(token);
        //    var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

        //    var resetUrl = $"{_frontendBaseUrl}?email={forgetpasswordDTO.Email}&token={encodedToken}";

        //    await _emailService.SendEmailAsync(forgetpasswordDTO.Email, "Reset Password", $"Click here: {resetUrl}");

        //    return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
        //    {
        //        Message = "There is a link have been sent to your email please check"
        //    });

        //}

        public async Task<ApiResponse<ConfirmationResponseDTO>> ForgetPassword(ForgetPasswordDTO forgetpasswordDTO)
        {

            var user = await _userManager.FindByEmailAsync(forgetpasswordDTO.Email);

            //if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            //    return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid request"); // Prevent account enumeration

            if (user == null)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Email Not Found, Go to register"); // Prevent account enumeration

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var encodedToken = WebUtility.UrlEncode(token);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));

            var resetUrl = $"{_frontendBaseUrl}?email={forgetpasswordDTO.Email}&token={encodedToken}";

            var content = $@"
                    <p style='color:#555;'>
                      تلقّينا طلبًا لإعادة تعيين كلمة المرور الخاصة بحسابك.
                      اضغط على الزر أدناه للمتابعة.
                    </p>

                    <div style='text-align:center; margin:30px 0;'>
                      <a href='{resetUrl}'
                         style='background-color:#2563eb;
                                color:#ffffff;
                                padding:14px 24px;
                                text-decoration:none;
                                font-weight:bold;
                                border-radius:6px;
                                display:inline-block;'>
                        إعادة تعيين كلمة المرور
                      </a>
                    </div>

                    <p style='color:#888; font-size:14px;'>
                      إذا لم تقم بطلب إعادة تعيين كلمة المرور، يمكنك تجاهل هذه الرسالة بأمان.
                    </p>";


            var htmlBody = BuildArabicTemplate("إعادة تعيين كلمة المرور", content);

            await _emailService.SendEmailAsync(forgetpasswordDTO.Email, "Reset Password", htmlBody);

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "There is a link have been sent to your email please check"
            });

        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> ResetPassword(ResetPasswordDTO resetpasswordDTO)
        {

            var user = await _userManager.FindByEmailAsync(resetpasswordDTO.Email);
            if (user == null)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid request");

            //var decodedToken = WebUtility.UrlDecode(resetpasswordDTO.Token);
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(resetpasswordDTO.Token));

            var result = await _userManager.ResetPasswordAsync(user, decodedToken, resetpasswordDTO.NewPassword);

            if (!result.Succeeded)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid request");

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Password reset successfully"
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

                await _userManager.AddToRoleAsync(user, "Company");
                var company = _context.TbEmployers.FirstOrDefault(c => c.UserId == user.Id);

                if (company is null)
                    return new ApiResponse<EmployerResponseDTO>(400, "Company logo not found.");

                if (company.LogoUrl is null)
                {
                    return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
                    {
                        EmployerId = company.Id,
                        Email = user.Email,
                        Role = "Company",
                        CompanyName = company.ComapnyName
                    });
                }

                var downLoadResult = _fileService.DownloadUrlAsync(company.LogoUrl);
                return new ApiResponse<EmployerResponseDTO>(200, new EmployerResponseDTO
                {
                    EmployerId = company.Id,
                    Email = user.Email,
                    SasUrl = downLoadResult.SasUrl,
                    ExpiresAt = downLoadResult.ExpiresAt,
                    Role = "Company",
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
