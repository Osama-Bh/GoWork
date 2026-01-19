using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class SeekerSkill
    {
        [Key]
        public int SeekerId { get; set; }
        public int SkillId { get; set; }

        // Navigation
        public Seeker Seeker { get; set; }
        public Skill Skill { get; set; }
    }
}