namespace GoWork.DTOs.InterviewDTOs
{
    public class InterviewStatisticsDTO
    {
        public int Scheduled { get; set; }
        public int Confirmed { get; set; }
        public int Completed { get; set; }
        public int Rescheduled { get; set; }
        public int Cancelled { get; set; }
        public int NoShow { get; set; }
    }
}
