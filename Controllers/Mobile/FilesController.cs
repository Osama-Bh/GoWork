using ECommerceApp.DTOs;
using GoWork.Data;
using GoWork.DTOs.FileDTOs;
using GoWork.Services.FileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoWork.Controllers.Mobile
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileService _fileService;

        public FilesController(ApplicationDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        [HttpPost("Upload")]
        public async Task<ActionResult<ApiResponse<ConfirmationResponseDTO>>> UploadFile(UploadFileRequestDTO fileRequestDTO)
        {
            var response = await _fileService.UploadAsync(fileRequestDTO);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }

        [HttpGet("ProfilePhoto")]
        public ActionResult<ApiResponse<FileDownloadDto>> DownloadFile(DownloadFileRequestDTO fileRequestDTO)
        {
            var response = _fileService.DownloadUrlAsync(fileRequestDTO.UserId);
            if (response.StatusCode != 200)
            {
                return StatusCode((int)response.StatusCode, response);
            }
            return Ok(response);
        }
    }
}
