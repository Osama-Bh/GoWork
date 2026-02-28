namespace GoWork.DTOs.AuthDTOs
{
    public class EmployerResponseDTO
    {
        public int EmployerId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? SasUrl { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow;
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber {  get; set; } = string.Empty;
        public string Industry {  get; set; } = string.Empty;
        public string Status {  get; set; } = string.Empty;
    }
}
