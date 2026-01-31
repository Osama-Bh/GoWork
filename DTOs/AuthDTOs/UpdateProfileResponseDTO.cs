namespace GoWork.DTOs.AuthDTOs
{
    public class UpdateProfileResponseDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Emial { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow;
        public List<string> Skills { get; set; } = new List<string>();
        public string Role { get; set; } = string.Empty;
    }
}
