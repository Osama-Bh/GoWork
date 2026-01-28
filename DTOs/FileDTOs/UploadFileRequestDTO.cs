namespace GoWork.DTOs.FileDTOs
{
    public class UploadFileRequestDTO
    {
        public IFormFile File { get; set; } = null!;
        public int UserId { get; set; }
    }
}
