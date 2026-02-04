namespace GoWork.DTOs.AuthDTOs
{
    public class CandidateResponseDTO
    {
        public CandidateResponseDTO()
        {
            Skills = new List<string>();
        }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string? ProfilPhotoUrl { get; set; }
        public string? ResumeUrl { get; set; }
        public List<string> Skills { get; set; }
    }
}
