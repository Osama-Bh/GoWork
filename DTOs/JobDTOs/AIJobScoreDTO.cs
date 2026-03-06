using System.Text.Json.Serialization;

namespace GoWork.DTOs.JobDTOs
{
    public class AIJobScoreDTO
    {
        [JsonPropertyName("job_id")]
        public int JobId { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }
    }

    public class AIJobRankingResponseDTO
    {
        [JsonPropertyName("ranked_jobs")]
        public List<AIJobScoreDTO> RankedJobs { get; set; } = new();
    }
}
