namespace GoWork.DTOs.AuthDTOs
{
    public class CandidateResponseDTO2
    {
        public int CandidateId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string SasUrl { get; set; } = string.Empty;
        public DateTimeOffset ExpiresAt { get; set; } = DateTimeOffset.UtcNow;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
