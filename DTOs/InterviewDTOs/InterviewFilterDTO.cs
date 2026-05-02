namespace GoWork.DTOs.InterviewDTOs
{
    public class InterviewFilterDTO
    {
        public string? SearchTerm { get; set; }
        public int? StatusId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
