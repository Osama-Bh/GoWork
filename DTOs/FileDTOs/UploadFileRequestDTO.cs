using GoWork.Enums;

namespace GoWork.DTOs.FileDTOs
{
    public class UploadFileRequestDTO
    {
        public IFormFile File { get; set; } = null!;
        public FileCategoryEnum FileCategory { get; set; }
    }
}
