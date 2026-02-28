using ECommerceApp.DTOs;
using GoWork.DTOs.DashboardDTOs;
using GoWork.Services.AdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoWork.Controllers.Dashboard
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SubAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Get company statistics for dashboard cards.
        /// </summary>
        [HttpGet("statistics")]
        public async Task<ActionResult<ApiResponse<CompanyStatisticsDTO>>> GetStatistics()
        {
            var response = await _adminService.GetCompanyStatisticsAsync();

            if(response.StatusCode!=200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Get paginated list of companies with search and filter support.
        /// </summary>
        [HttpGet("companies")]
        public async Task<ActionResult<ApiResponse<PaginatedResult<CompanyListItemDTO>>>> GetCompanies(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? status = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? sortOrder = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 50) pageSize = 10;

            var response = await _adminService.GetCompaniesAsync(page, pageSize, search, status, sortBy, sortOrder);

            if(response.StatusCode!=200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Get detailed information for a single company.
        /// </summary>
        [HttpGet("companies/{id}")]
        public async Task<ActionResult<ApiResponse<CompanyDetailDTO>>> GetCompanyById(int id)
        {
            var response = await _adminService.GetCompanyByIdAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Update company status (Approve / Reject / Suspend).
        /// </summary>
        [HttpPatch("companies/{id}/status")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCompanyStatus(int id, UpdateCompanyStatusDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }

            var response = await _adminService.UpdateCompanyStatusAsync(id, dto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Soft delete a company (sets status to Suspended).
        /// </summary>
        [HttpDelete("companies/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> DeleteCompany(int id)
        {
            var response = await _adminService.DeleteCompanyAsync(id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Update company details (name, industry, email, phone).
        /// </summary>
        [HttpPut("companies/{id}")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UpdateCompany(int id, AdminUpdateCompanyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }

            var response = await _adminService.UpdateCompanyAsync(id, dto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Perform bulk actions on multiple companies (Approve, Reject, Suspend, Delete).
        /// </summary>
        [HttpPost("companies/bulk-action")]
        public async Task<ActionResult<ApiResponse<BulkActionResultDTO>>> BulkAction(BulkActionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request data.");
            }

            var response = await _adminService.BulkActionAsync(dto);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
