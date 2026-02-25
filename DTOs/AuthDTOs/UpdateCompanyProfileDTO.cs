namespace GoWork.DTOs.AuthDTOs
{
    public class UpdateCompanyProfileDTO
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public IFormFile? LogoUrl { get; set; }
        public string CompanyName { get; set; } = string.Empty ;
        public bool isLogoChanged { get; set; } 
        public bool isLogoDeleted { get; set; }
    }
}
