using GoWork.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GoWork.Models
{

    public class EmployerStatus
    {
        public int Id { get; set; }              // PK, matches enum
        public string Name { get; set; } = null!;   // "Pending", "Active", "Suspended"
        public int SortOrder { get; set; }       // UI display order
        public bool IsActive { get; set; } = true;

        public ICollection<Employer> Employers { get; set; } = new List<Employer>();
    }


}
