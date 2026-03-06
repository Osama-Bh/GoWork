namespace GoWork.DTOs.JobDTOs
{
    public class JobSearchRequestDto
    {
        public string? Search { get; set; }
        public int? CategoryId { get; set; }
        public int? JobTypeId { get; set; }
        public int? JobLocationTypeId { get; set; }
        public int? CountryId { get; set; }
        
        // Sorting flags: "date", "salary", "title"
        public string? SortBy { get; set; } 
        // "asc" or "desc"
        public string? SortOrder { get; set; } 

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 30;

        [System.Text.Json.Serialization.JsonIgnore]
        public int? SeekerId { get; set; }
    }
}
