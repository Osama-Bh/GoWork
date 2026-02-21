namespace GoWork.DTOs.AuthDTOs
{
    public class UpdateCompanyProfileDTO
    {
        public string Phone { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public IFormFile? LogoUrl { get; set; }
    }
}
