namespace GoWork.DTOs.FileDTOs
{
    public class FileDownloadDto
    {
        public string SasUrl { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow;
        public bool Succeeded { get; set; } = false;
    }
}
