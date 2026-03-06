namespace GoWork.DTOs.JobDTOs
{
    public class JobSearchResponseDto
    {
        public List<JobCardDto> Jobs { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasNextPage { get; set; }
    }
}
