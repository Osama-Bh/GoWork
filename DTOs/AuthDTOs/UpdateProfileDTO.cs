namespace GoWork.DTOs.AuthDTOs
{
    public class UpdateProfileDTO
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNo { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
        public IFormFile? ResumeFile { get; set; }
        public List<string>? Skills { get; set; }

    }
}
