using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWork.Models
{
    public class Application
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
        public int SeekerId { get; set; }
        [ForeignKey("SeekerId")]
        public Seeker Seeker { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationStatusId { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public ICollection<Interview>? Interviews { get; set; }

    }
}
