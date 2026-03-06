using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.DashboardDTOs;
using GoWork.DTOs.JobDTOs;
using GoWork.Enums;
using GoWork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.Text.Json;

namespace GoWork.Services.JobService
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public JobService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // ==================== Job CRUD ====================

        public async Task<ApiResponse<PaginatedResult<JobListItemDTO>>> GetJobsAsync(
            int employerId, int page, int pageSize, string? search, string? status, int? jobTypeId)
        {
            await CheckAndExpireJobsAsync(employerId);

            var query = _context.TbJobs
                .Where(j => j.EmployerId == employerId)
                .Include(j => j.JobType)
                .Include(j => j.Currency)
                .Include(j => j.JobStatus)
                .Include(j => j.Applications)
                .AsQueryable();

            // Search by title
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(j => j.Title.Contains(search));
            }

            // Filter by status
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (Enum.TryParse<JobStatusEnum>(status, true, out var statusEnum))
                {
                    query = query.Where(j => j.JobStatusId == (int)statusEnum);
                }
            }

            // Filter by job type
            if (jobTypeId.HasValue)
            {
                query = query.Where(j => j.JobTypeId == jobTypeId.Value);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(j => j.PostedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(j => new JobListItemDTO
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    JobType = j.JobType.Name,
                    MinSalary = j.MinSalary,
                    MaxSalary = j.MaxSalary,
                    Currency = j.Currency.Name,
                    PostedDate = j.PostedDate,
                    ExpirationDate = j.ExpirationDate,
                    ApplicantsCount = j.Applications != null ? j.Applications.Count : 0,
                    Status = j.JobStatus.Name
                })
                .ToListAsync();

            var result = new PaginatedResult<JobListItemDTO>
            {
                Items = items,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return new ApiResponse<PaginatedResult<JobListItemDTO>>(200, result);
        }

        public async Task<ApiResponse<JobDetailDTO>> GetJobByIdAsync(int employerId, int id)
        {
            await CheckAndExpireJobsAsync(employerId);

            var job = await _context.TbJobs
                .Where(j => j.Id == id && j.EmployerId == employerId)
                .Include(j => j.JobType)
                .Include(j => j.Category)
                .Include(j => j.JobLocationType)
                .Include(j => j.Currency)
                .Include(j => j.JobStatus)
                .Include(j => j.Address).ThenInclude(a => a.Country)
                .Include(j => j.Address).ThenInclude(a => a.Governate)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.Applications)
                .FirstOrDefaultAsync();

            if (job == null)
                return new ApiResponse<JobDetailDTO>(404, "Job not found.");

            var detail = new JobDetailDTO
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                JobTypeId = job.JobTypeId,
                JobType = job.JobType.Name,
                CategoryId = job.CategoryId,
                Category = job.Category.Name,
                JobLocationTypeId = job.JobLocationTypeId,
                JobLocationType = job.JobLocationType.Name,
                CurrencyId = job.CurrencyId,
                Currency = job.Currency.Name,
                MinSalary = job.MinSalary,
                MaxSalary = job.MaxSalary,
                PostedDate = job.PostedDate,
                ExpirationDate = job.ExpirationDate,
                ApplicantsCount = job.Applications?.Count ?? 0,
                Status = job.JobStatus.Name,
                AddressLine = job.Address.AddressLine1,
                CountryId = job.Address.CountryId,
                Country = job.Address.Country.Name,
                GovernateId = job.Address.GovernateId,
                Governate = job.Address.Governate.Name,
                Skills = job.JobSkills?.Select(js => new SkillDTO
                {
                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                }).ToList() ?? new()
            };

            return new ApiResponse<JobDetailDTO>(200, detail);
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> CreateJobAsync(int employerId, CreateJobDTO dto)
        {
            // Validate expiration date is in the future
            if (dto.ExpirationDate <= DateTime.UtcNow)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Expiration date must be in the future.");

            if (dto.MinSalary > dto.MaxSalary)
                return new ApiResponse<ConfirmationResponseDTO>(400, "Min salary cannot be greater than max salary.");

            // Create address
            var address = new Address
            {
                AddressLine1 = dto.AddressLine ?? string.Empty,
                CountryId = dto.CountryId,
                GovernateId = dto.GovernateId
            };
            _context.TbAddresses.Add(address);
            await _context.SaveChangesAsync();

            // Create job
            var job = new Job
            {
                Title = dto.Title,
                Description = dto.Description,
                EmployerId = employerId,
                CategoryId = dto.CategoryId,
                JobTypeId = dto.JobTypeId,
                JobLocationTypeId = dto.JobLocationTypeId,
                AddressId = address.Id,
                MinSalary = dto.MinSalary,
                MaxSalary = dto.MaxSalary,
                CurrencyId = dto.CurrencyId,
                PostedDate = DateTime.UtcNow,
                ExpirationDate = dto.ExpirationDate,
                JobStatusId = (int)JobStatusEnum.Published
            };
            _context.TbJobs.Add(job);
            await _context.SaveChangesAsync();

            // Handle skills
            await HandleJobSkillsAsync(job.Id, dto.SkillIds, dto.NewSkills);

            return new ApiResponse<ConfirmationResponseDTO>(201, new ConfirmationResponseDTO
            {
                Message = "Job created successfully."
            });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateJobAsync(int employerId, int id, UpdateJobDTO dto)
        {
            var job = await _context.TbJobs
                .Include(j => j.Address)
                .FirstOrDefaultAsync(j => j.Id == id && j.EmployerId == employerId);

            if (job == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Job not found.");

            if (dto.Title != null) job.Title = dto.Title;
            if (dto.Description != null) job.Description = dto.Description;
            if (dto.JobTypeId.HasValue) job.JobTypeId = dto.JobTypeId.Value;
            if (dto.CategoryId.HasValue) job.CategoryId = dto.CategoryId.Value;
            if (dto.JobLocationTypeId.HasValue) job.JobLocationTypeId = dto.JobLocationTypeId.Value;
            if (dto.CurrencyId.HasValue) job.CurrencyId = dto.CurrencyId.Value;
            if (dto.MinSalary.HasValue) job.MinSalary = dto.MinSalary.Value;
            if (dto.MaxSalary.HasValue) job.MaxSalary = dto.MaxSalary.Value;
            if (dto.ExpirationDate.HasValue)
            {
                job.ExpirationDate = dto.ExpirationDate.Value;

                // Auto-republish if the job was previously expired and the new date is in the future
                if (job.JobStatusId == (int)JobStatusEnum.Expired && job.ExpirationDate >= DateTime.UtcNow)
                {
                    job.JobStatusId = (int)JobStatusEnum.Published;
                }
            }

            // Update address if location fields provided
            if (dto.CountryId.HasValue || dto.GovernateId.HasValue || dto.AddressLine != null)
            {
                if (dto.CountryId.HasValue) job.Address.CountryId = dto.CountryId.Value;
                if (dto.GovernateId.HasValue) job.Address.GovernateId = dto.GovernateId.Value;
                if (dto.AddressLine != null) job.Address.AddressLine1 = dto.AddressLine;
            }

            // Update skills if provided
            if (dto.SkillIds != null || dto.NewSkills != null)
            {
                // Remove existing skills
                var existingSkills = await _context.TbJobSkills.Where(js => js.JobId == id).ToListAsync();
                _context.TbJobSkills.RemoveRange(existingSkills);
                await _context.SaveChangesAsync();

                await HandleJobSkillsAsync(id, dto.SkillIds ?? new(), dto.NewSkills ?? new());
            }

            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Job updated successfully."
            });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateJobStatusAsync(int employerId, int id, UpdateJobStatusDTO dto)
        {
            if (!Enum.TryParse<JobStatusEnum>(dto.Status, true, out var newStatus))
                return new ApiResponse<ConfirmationResponseDTO>(400, "Invalid status. Allowed: Published, Closed.");

            if (newStatus != JobStatusEnum.Published && newStatus != JobStatusEnum.Closed)
                return new ApiResponse<ConfirmationResponseDTO>(400, "You can only set status to Published or Closed.");

            var job = await _context.TbJobs.FirstOrDefaultAsync(j => j.Id == id && j.EmployerId == employerId);
            if (job == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Job not found.");

            if (newStatus == JobStatusEnum.Published && job.ExpirationDate < DateTime.UtcNow)
            {
                return new ApiResponse<ConfirmationResponseDTO>(400, "Cannot publish a job with an expiration date in the past. Please update the expiration date first.");
            }

            job.JobStatusId = (int)newStatus;
            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Job status updated successfully."
            });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteJobAsync(int employerId, int id)
        {
            var job = await _context.TbJobs
                .Include(j => j.JobSkills)
                .Include(j => j.Applications)!.ThenInclude(a => a.Interviews)
                .FirstOrDefaultAsync(j => j.Id == id && j.EmployerId == employerId);

            if (job == null)
                return new ApiResponse<ConfirmationResponseDTO>(404, "Job not found.");

            // Delete bottom-up: Interviews → Applications → JobSkills → Job
            if (job.Applications != null)
            {
                foreach (var app in job.Applications)
                {
                    if (app.Interviews != null)
                        _context.TbInterviews.RemoveRange(app.Interviews);
                }
                _context.TbApplications.RemoveRange(job.Applications);
            }

            if (job.JobSkills != null)
                _context.TbJobSkills.RemoveRange(job.JobSkills);

            _context.TbJobs.Remove(job);
            await _context.SaveChangesAsync();

            return new ApiResponse<ConfirmationResponseDTO>(200, new ConfirmationResponseDTO
            {
                Message = "Job deleted successfully."
            });
        }

        // ==================== AI Recommendations ====================

        public async Task<ApiResponse<List<JobRecommendationResponseDTO>>> GetJobRecommendationsAsync(int seekerId)
        {
            // 1. Fetch seeker and validate
            var seeker = await _context.TbSeekers
                .Include(s => s.InterestCategory)
                .Include(s => s.SeekerSkills).ThenInclude(ss => ss.Skill)
                .FirstOrDefaultAsync(s => s.Id == seekerId);

            if (seeker == null)
            {
                return new ApiResponse<List<JobRecommendationResponseDTO>>(404, "Seeker not found.");
            }

            // 2. Fetch pre-filtered jobs via SP
            var preFilteredJobs = await _context.Database.SqlQueryRaw<PreFilteredJobDTO>(
                "EXEC sp_GetPreFilteredJobs_ForAI @p0", seekerId)
                .AsNoTracking()
                .ToListAsync();

            if (!preFilteredJobs.Any())
            {
                return new ApiResponse<List<JobRecommendationResponseDTO>>(200, new List<JobRecommendationResponseDTO>());
            }

            // Default fallback response if AI fails
            var fallbackResponse = preFilteredJobs.Select(j => new JobRecommendationResponseDTO
            {
                Id = j.Id,
                Title = j.Title,
                Description = j.Description,
                MinSalary = j.MinSalary,
                MaxSalary = j.MaxSalary,
                CategoryName = j.CategoryName,
                RequiredSkills = string.IsNullOrWhiteSpace(j.RequiredSkills) ? new() : j.RequiredSkills.Split(',').Select(s => s.Trim()).ToList(),
                Score = null
            }).OrderByDescending(j => j.Id).ToList(); // Sort by ID or Date as fallback

            // 3. Setup AI
            var apiKey = _configuration["OpenAI:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                // Fallback gracefully if no key is provided
                return new ApiResponse<List<JobRecommendationResponseDTO>>(200, fallbackResponse);
            }

            try
            {
                // 4. Construct JSON objects for prompt
                var candidateProfile = new
                {
                    skills = seeker.SeekerSkills?.Select(ss => ss.Skill.Name).ToList() ?? new List<string>(),
                    category = seeker.InterestCategory?.Name ?? "General"
                };

                var jobsList = preFilteredJobs.Select(j => new
                {
                    job_id = j.Id,
                    title = j.Title,
                    description = j.Description,
                    required_skills = string.IsNullOrWhiteSpace(j.RequiredSkills) ? new List<string>() : j.RequiredSkills.Split(',').Select(s => s.Trim()).ToList()
                });

                var candidateJson = JsonSerializer.Serialize(candidateProfile);
                var jobsJson = JsonSerializer.Serialize(jobsList);

                var prompt = $@"
                You are a job ranking AI. Rank the following jobs for the candidate based on:
                1. Skills overlap (highest priority)
                2. Semantic similarity between job description and candidate skills

                Return ONLY valid JSON in this exact format:

                {{
                  ""ranked_jobs"": [
                    {{ ""job_id"": 10, ""score"": 0.92 }},
                    {{ ""job_id"": 11, ""score"": 0.81 }}
                  ]
                }}

                Do not include explanations or text outside JSON.

                Candidate:
                {candidateJson}

                Jobs:
                {jobsJson}";

                // 5. Build and call OpenAI SDK (v2.0.0+)
                var modelName = _configuration["OpenAI:Model"] ?? "gpt-4o-mini";
                var chatClient = new ChatClient(modelName, apiKey);
                
                var options = new ChatCompletionOptions
                {
                    Temperature = 0.2f,
                    ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat()
                };

                var completion = await chatClient.CompleteChatAsync(new ChatMessage[] { new SystemChatMessage(prompt) }, options);
                
                var aiContent = completion.Value.Content[0].Text;

                var resultDto = JsonSerializer.Deserialize<AIJobRankingResponseDTO>(aiContent);
                if (resultDto?.RankedJobs == null || !resultDto.RankedJobs.Any())
                {
                    return new ApiResponse<List<JobRecommendationResponseDTO>>(200, fallbackResponse);
                }

                // 6. Merge scores and return sorted
                var finalRecommendations = preFilteredJobs.Select(j =>
                {
                    var ranking = resultDto.RankedJobs.FirstOrDefault(r => r.JobId == j.Id);
                    return new JobRecommendationResponseDTO
                    {
                        Id = j.Id,
                        Title = j.Title,
                        Description = j.Description,
                        MinSalary = j.MinSalary,
                        MaxSalary = j.MaxSalary,
                        CategoryName = j.CategoryName,
                        RequiredSkills = string.IsNullOrWhiteSpace(j.RequiredSkills) ? new() : j.RequiredSkills.Split(',').Select(s => s.Trim()).ToList(),
                        Score = ranking?.Score
                    };
                })
                .OrderByDescending(j => j.Score ?? -1)
                .ToList();

                return new ApiResponse<List<JobRecommendationResponseDTO>>(200, finalRecommendations);

            }
            catch (Exception ex)
            {
                // Fallback gracefully on exception
                Console.WriteLine($"AI Recommendation Failed: {ex.Message}");
                return new ApiResponse<List<JobRecommendationResponseDTO>>(200, fallbackResponse);
            }
        }

        // ==================== Lookups ====================

        public async Task<ApiResponse<List<LookupDTO>>> GetCategoriesAsync(string? search)
        {
            var query = _context.TbCategories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search));
            }

            var items = await query
                .OrderBy(c => c.Name)
                .Select(c => new LookupDTO { Id = c.Id, Name = c.Name })
                .ToListAsync();
            return new ApiResponse<List<LookupDTO>>(200, items);
        }

        public async Task<ApiResponse<List<LookupDTO>>> GetJobTypesAsync()
        {
            var items = await _context.TbJobTypes
                .Where(jt => jt.IsActive)
                .OrderBy(jt => jt.SortOrder)
                .Select(jt => new LookupDTO { Id = jt.Id, Name = jt.Name })
                .ToListAsync();
            return new ApiResponse<List<LookupDTO>>(200, items);
        }

        public async Task<ApiResponse<List<LookupDTO>>> GetLocationTypesAsync()
        {
            var items = await _context.TbJobLocationTypes
                .Select(lt => new LookupDTO { Id = lt.Id, Name = lt.Name })
                .ToListAsync();
            return new ApiResponse<List<LookupDTO>>(200, items);
        }

        public async Task<ApiResponse<List<CurrencyLookupDTO>>> GetCurrenciesAsync(string? search)
        {
            var query = _context.TbCurrencies.Where(c => c.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search) || c.Code.Contains(search));
            }

            var items = await query
                .OrderBy(c => c.Name)
                .Select(c => new CurrencyLookupDTO { Id = c.Id, Code = c.Code, Name = c.Name })
                .ToListAsync();
            return new ApiResponse<List<CurrencyLookupDTO>>(200, items);
        }

        public async Task<ApiResponse<List<CountryLookupDTO>>> GetCountriesAsync(string? search)
        {
            var query = _context.TbCountries.Where(c => c.IsActive);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.Name.Contains(search) || c.Code.Contains(search));
            }

            var items = await query
                .OrderBy(c => c.Name)
                .Select(c => new CountryLookupDTO { Id = c.Id, Name = c.Name, Code = c.Code })
                .ToListAsync();
            return new ApiResponse<List<CountryLookupDTO>>(200, items);
        }

        public async Task<ApiResponse<List<LookupDTO>>> GetGovernatesAsync(int countryId, string? search)
        {
            var query = _context.TbGovernates.Where(g => g.CountryId == countryId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(g => g.Name.Contains(search));
            }

            var items = await query
                .OrderBy(g => g.Name)
                .Select(g => new LookupDTO { Id = g.Id, Name = g.Name })
                .ToListAsync();
            return new ApiResponse<List<LookupDTO>>(200, items);
        }

        public async Task<ApiResponse<List<SkillDTO>>> GetSkillsAsync(string? search)
        {
            var query = _context.TbSkills.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            var items = await query
                .OrderBy(s => s.Name)
                .Take(50)
                .Select(s => new SkillDTO { Id = s.Id, Name = s.Name })
                .ToListAsync();

            return new ApiResponse<List<SkillDTO>>(200, items);
        }

        // ==================== Helpers ====================

        private async Task HandleJobSkillsAsync(int jobId, List<int> skillIds, List<string> newSkillNames)
        {
            // Add existing skills
            foreach (var skillId in skillIds)
            {
                var skillExists = await _context.TbSkills.AnyAsync(s => s.Id == skillId);
                if (skillExists)
                {
                    _context.TbJobSkills.Add(new JobSkill { JobId = jobId, SkillId = skillId });
                }
            }

            // Create and add new skills
            foreach (var skillName in newSkillNames)
            {
                if (string.IsNullOrWhiteSpace(skillName)) continue;

                // Check if skill already exists by name
                var existing = await _context.TbSkills.FirstOrDefaultAsync(s => s.Name == skillName);
                if (existing != null)
                {
                    _context.TbJobSkills.Add(new JobSkill { JobId = jobId, SkillId = existing.Id });
                }
                else
                {
                    var newSkill = new Skill { Name = skillName };
                    _context.TbSkills.Add(newSkill);
                    await _context.SaveChangesAsync();
                    _context.TbJobSkills.Add(new JobSkill { JobId = jobId, SkillId = newSkill.Id });
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task CheckAndExpireJobsAsync(int employerId)
        {
            var expiredJobs = await _context.TbJobs
                .Where(j => j.EmployerId == employerId &&
                            j.JobStatusId == (int)JobStatusEnum.Published &&
                            j.ExpirationDate < DateTime.UtcNow)
                .ToListAsync();

            if (expiredJobs.Any())
            {
                foreach (var job in expiredJobs)
                {
                    job.JobStatusId = (int)JobStatusEnum.Expired;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
