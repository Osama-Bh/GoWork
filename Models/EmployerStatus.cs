using GoWork.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GoWork.Models
{
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmployerStatus
    {
        PendingForApproval = 1,
        Approved = 2,
        Rejected = 3,
    }
  
}
