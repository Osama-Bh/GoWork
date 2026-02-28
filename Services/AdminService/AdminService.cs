using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.DashboardDTOs;
using GoWork.Enums;
using GoWork.Services.EmailService;
using GoWork.Services.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GoWork.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;

        public AdminService(ApplicationDbContext context, UserManager<ApplicationUser> userManager,IFileService fileService, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _fileService = fileService;
            _emailService = emailService;
        }

        public async Task<ApiResponse<CompanyStatisticsDTO>> GetCompanyStatisticsAsync()
        {
            var employers = _context.TbEmployers;

            var stats = new CompanyStatisticsDTO
            {
                TotalCompanies = await employers.CountAsync(),
                PendingVerification = await employers.CountAsync(e => e.EmployerStatusId == (int)EmployerStatusEnum.PendingApproval),
                Verified = await employers.CountAsync(e => e.EmployerStatusId == (int)EmployerStatusEnum.Active),
                Rejected = await employers.CountAsync(e => e.EmployerStatusId == (int)EmployerStatusEnum.Rejected)
            };

            return new ApiResponse<CompanyStatisticsDTO>(200, stats);
        }

        public async Task<ApiResponse<PaginatedResult<CompanyListItemDTO>>> GetCompaniesAsync(
            int page, int pageSize, string? search, string? status, string? sortBy, string? sortOrder)
        {
            var query = _context.TbEmployers
                .Include(e => e.ApplicationUser)
                .Include(e => e.EmployerStatus)
                .AsQueryable();

            // Search by company name
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.ComapnyName.Contains(search));
            }

            // Filter by status
            if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<EmployerStatusEnum>(status, true, out var statusEnum))
            {
                query = query.Where(e => e.EmployerStatusId == (int)statusEnum);
            }

            // Sorting
            query = (sortBy?.ToLower(), sortOrder?.ToLower()) switch
            {
                ("name", "asc") => query.OrderBy(e => e.ComapnyName),
                ("name", _) => query.OrderByDescending(e => e.ComapnyName),
                ("email", "asc") => query.OrderBy(e => e.ApplicationUser.Email),
                ("email", _) => query.OrderByDescending(e => e.ApplicationUser.Email),
                ("status", "asc") => query.OrderBy(e => e.EmployerStatus.Name),
                ("status", _) => query.OrderByDescending(e => e.EmployerStatus.Name),
                (_, "asc") => query.OrderBy(e => e.CreatedAt),
                _ => query.OrderByDescending(e => e.CreatedAt)
            };

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new CompanyListItemDTO
                {
                    Id = e.Id,
                    CompanyName = e.ComapnyName,
                    Industry = e.Industry,
                    Email = e.ApplicationUser.Email ?? string.Empty,
                    PhoneNumber = e.ApplicationUser.PhoneNumber ?? string.Empty,
                    CreatedAt = e.CreatedAt,
                    Status = e.EmployerStatus.Name,
                    LogoUrl = _fileService.DownloadUrlAsync(e.LogoUrl).SasUrl
                })
                .ToListAsync();

            var result = new PaginatedResult<CompanyListItemDTO>
            {
                Items = items,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return new ApiResponse<PaginatedResult<CompanyListItemDTO>>(200, result);
        }

        public async Task<ApiResponse<CompanyDetailDTO>> GetCompanyByIdAsync(int id)
        {
            var employer = await _context.TbEmployers
                .Include(e => e.ApplicationUser)
                .Include(e => e.EmployerStatus)
                .Include(e => e.Address)
                    .ThenInclude(a => a!.Governate)
                .Include(e => e.Jobs)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employer == null)
            {
                return new ApiResponse<CompanyDetailDTO>(404, "Company not found.");
            }
            var logoUrlResponse = _fileService
                    .DownloadUrlAsync(employer.LogoUrl);

            var detail = new CompanyDetailDTO
            {
                Id = employer.Id,
                CompanyName = employer.ComapnyName,
                Industry = employer.Industry,
                Email = employer.ApplicationUser.Email ?? string.Empty,
                PhoneNumber = employer.ApplicationUser.PhoneNumber ?? string.Empty,
                CreatedAt = employer.CreatedAt,
                Status = employer.EmployerStatus.Name,
                LogoUrl = logoUrlResponse.SasUrl,
                EmailConfirmed = employer.ApplicationUser.EmailConfirmed,
                TotalJobs = employer.Jobs?.Count ?? 0,
                City = employer.Address?.AddressLine1,
                Governate = employer.Address?.Governate?.Name
            };

            return new ApiResponse<CompanyDetailDTO>(200, detail);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCompanyStatusAsync(int id, UpdateCompanyStatusDTO dto)
        {
            if (!Enum.TryParse<EmployerStatusEnum>(dto.Status, true, out var newStatus))
            {
                return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid status value.");
            }

            //var employer = await _context.TbEmployers.FindAsync(id);

            var employer = await _context.TbEmployers
            .Include(e => e.ApplicationUser)
            .FirstOrDefaultAsync(e => e.Id == id);

            if (employer == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "Company not found.");
            }

            // Prevent unnecessary update
            if (employer.EmployerStatusId == (int)newStatus)
            {
                return new ApiResponse<ConfirmationResponseDTO>(400, "Status is already set to this value.");
            }

            employer.EmployerStatusId = (int)newStatus;
            await _context.SaveChangesAsync();

            // 🔔 Send Email Notification
            var userEmail = employer.ApplicationUser.Email;
            var companyName = employer.ComapnyName;

            var subject = "Company Status Updated";

            var body = $@"
            <p>Dear {companyName},</p>
            <p>Your company status has been updated.</p>
            <p><strong>New Status:</strong> {newStatus}</p>
        ";

            await _emailService.SendEmailAsync(userEmail, subject, body, companyName);

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Company status updated successfully."
            });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCompanyAsync(int id)
        {
            var employer = await _context.TbEmployers.FindAsync(id);
            if (employer == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "Company not found.");
            }

            // Soft delete: set status to Blocked
            employer.EmployerStatusId = (int)EmployerStatusEnum.Blocked;
            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Company has been blocked successfully."
            });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateCompanyAsync(int id, AdminUpdateCompanyDTO dto)
        {
            var employer = await _context.TbEmployers
                .Include(e => e.ApplicationUser)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employer == null)
            {
                return new ApiResponse<ConfirmationResponseDTO>(404, "Company not found.");
            }

            // Update only the fields that are provided
            if (!string.IsNullOrWhiteSpace(dto.CompanyName))
                employer.ComapnyName = dto.CompanyName;

            if (!string.IsNullOrWhiteSpace(dto.Industry))
                employer.Industry = dto.Industry;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                employer.ApplicationUser.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
                employer.ApplicationUser.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Company updated successfully."
            });
        }

        public async Task<ApiResponse<BulkActionResultDTO>> BulkActionAsync(BulkActionDTO dto)
        {
            if (dto.CompanyIds == null || dto.CompanyIds.Count == 0)
            {
                return new ApiResponse<BulkActionResultDTO>(400, "No companies selected.");
            }

            // Determine target status based on action
            int? targetStatusId = dto.Action?.ToLower() switch
            {
                "approve" => (int)EmployerStatusEnum.Active,
                "reject" => (int)EmployerStatusEnum.Rejected,
                //"suspend" or "delete" => (int)EmployerStatusEnum.Suspended,
                "suspend" => (int)EmployerStatusEnum.Suspended,
                "delete" => (int)EmployerStatusEnum.Blocked,
                _ => null
            };

            if (targetStatusId == null)
            {
                return new ApiResponse<BulkActionResultDTO>(400, "Invalid action. Allowed: Approve, Reject, Suspend, Delete.");
            }

            var employers = await _context.TbEmployers
                .Where(e => dto.CompanyIds.Contains(e.Id))
                .ToListAsync();

            int successCount = 0;
            int failedCount = 0;

            foreach (var employer in employers)
            {
                try
                {
                    employer.EmployerStatusId = targetStatusId.Value;
                    successCount++;
                }
                catch
                {
                    failedCount++;
                }
            }

            // Count IDs that were not found
            failedCount += dto.CompanyIds.Count - employers.Count;

            await _context.SaveChangesAsync();

            var result = new BulkActionResultDTO
            {
                SuccessCount = successCount,
                FailedCount = failedCount,
                Message = $"{successCount} companies processed successfully."
            };

            return new ApiResponse<BulkActionResultDTO>(200, result);
        }
    }
}
