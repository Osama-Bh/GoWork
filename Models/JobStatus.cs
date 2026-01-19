using System.Text.Json.Serialization;

namespace GoWork.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum JobStatus
    {
    }
}
