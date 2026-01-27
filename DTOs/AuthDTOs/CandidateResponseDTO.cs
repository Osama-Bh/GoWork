namespace GoWork.DTOs.AuthDTOs
{
    public class CandidateResponseDTO
    {
        public int CandidateId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
