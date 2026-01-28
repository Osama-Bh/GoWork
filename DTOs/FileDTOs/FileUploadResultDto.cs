namespace GoWork.DTOs.FileDTOs
{
    public class FileUploadResultDto
    {
        public string BlobUri { get; set; }  = string.Empty;
        public string BlobName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}
