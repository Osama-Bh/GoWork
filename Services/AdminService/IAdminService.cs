using ECommerceApp.DTOs;
using GoWork.DTOs.DashboardDTOs;

namespace GoWork.Services.AdminService
{
    public interface IAdminService
    {
        Task<ApiResponse<CompanyStatisticsDTO>> GetCompanyStatisticsAsync();

        Task<ApiResponse<PaginatedResult<CompanyListItemDTO>>> GetCompaniesAsync(
            int page, int pageSize, string? search, string? status, string? sortBy, string? sortOrder);

        Task<ApiResponse<CompanyDetailDTO>> GetCompanyByIdAsync(int id);

        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCompanyStatusAsync(int id, UpdateCompanyStatusDTO dto);

        Task<ApiResponse<ConfirmationResponseDTO>> DeleteCompanyAsync(int id);

        Task<ApiResponse<ConfirmationResponseDTO>> UpdateCompanyAsync(int id, AdminUpdateCompanyDTO dto);

        Task<ApiResponse<BulkActionResultDTO>> BulkActionAsync(BulkActionDTO dto);
    }
}
