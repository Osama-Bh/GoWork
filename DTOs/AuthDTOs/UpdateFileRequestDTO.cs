namespace GoWork.DTOs.AuthDTOs
{
    public class UpdateFileRequestDTO
    {
        public IFormFile File { get; set; }
        public int UserId { get; set; }
    }
}
