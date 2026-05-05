using System.Text.Json.Serialization;

namespace GoWork.DTOs.JobDTOs
{
    public class JobDescriptionEnhancementResultDTO
    {
        [JsonPropertyName("enhanced_description")]
        public string EnhancedDescription { get; set; } = string.Empty;
    }
}
