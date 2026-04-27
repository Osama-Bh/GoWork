using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs;
using GoWork.DTOs.ApplicationDTOs;
using GoWork.Enums;
using GoWork.Services.FileService;
using Microsoft.EntityFrameworkCore;

namespace GoWork.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public ApplicationService(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<ApiResponse<List<LookUpDTO>>> GetApplicationStatuses()
        {
            var items = await _context.TbApplicationStatuses
                .Where(a => a.IsActive)
                .OrderBy(a => a.SortOrder)
                .Select(a => new LookUpDTO
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();

            return new ApiResponse<List<LookUpDTO>>(200, items);
        }

        public async Task<ApiResponse<ApplicationsResponseDTO>> GetCandidateApplications(ApplicationsRequestDTO requestDTO)
        {
            if (!requestDTO.UserId.HasValue)
                return new ApiResponse<ApplicationsResponseDTO>(401, "unauthorized user");

            // Single async query for the seeker
            var seeker = await _context.TbSeekers
                .FirstOrDefaultAsync(s => s.UserId == requestDTO.UserId.Value);

            if (seeker == null)
                return new ApiResponse<ApplicationsResponseDTO>(404, "seeker not found");

            // Build base query filtered by seeker
            var baseQuery = _context.TbApplications.Where(a => a.SeekerId == seeker.Id);

            // Apply optional status filter
            if (requestDTO.ApplicationStatusId.HasValue)
            {
                baseQuery = baseQuery.Where(a => a.ApplicationStatusId == requestDTO.ApplicationStatusId.Value);
            }

            // Apply sorting (default: newest first)
            baseQuery = string.Equals(requestDTO.SortOrder, "desc", StringComparison.OrdinalIgnoreCase)
                ? baseQuery.OrderByDescending(a => a.ApplicationDate)
                : baseQuery.OrderBy(a => a.ApplicationDate);

            // Count BEFORE pagination
            var totalCount = await baseQuery.CountAsync();

            // Apply pagination
            var pagedApplications = await baseQuery
                .Skip((requestDTO.Page - 1) * requestDTO.PageSize)
                .Take(requestDTO.PageSize)
                .Select(a => new ApplicationDTO
                {
                    ApplicationId = a.Id,
                    JobId = a.JobId,
                    JobTitle = a.Job.Title,
                    CompanyName = a.Job.Employer.ComapnyName,
                    CompanyLogo = a.Job.Employer.LogoUrl,
                    ApplicationStatus = a.ApplicationStatus.Name,
                    AppliedDate = a.ApplicationDate,
                    CanWithdraw = a.ApplicationStatusId == (int)ApplicationStatusEnum.PendingReview
                })
                .ToListAsync();

            // Resolve company logo URLs via file service
            foreach (var application in pagedApplications)
            {
                if (!string.IsNullOrEmpty(application.CompanyLogo))
                    application.CompanyLogo = _fileService.DownloadUrlAsync(application.CompanyLogo)?.SasUrl;
                else
                    application.CompanyLogo = null;
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / requestDTO.PageSize);

            return new ApiResponse<ApplicationsResponseDTO>(200, new ApplicationsResponseDTO
            {
                Applications = pagedApplications,
                PageNumber = requestDTO.Page,
                PageSize = requestDTO.PageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = requestDTO.Page < totalPages
            });
        }
    }
}
