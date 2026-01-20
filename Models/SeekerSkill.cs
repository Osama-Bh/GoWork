using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class SeekerSkill
    {
        public int Id { get; set; }
        public int SeekerId { get; set; }
        public int SkillId { get; set; }

        // Navigation
        public Seeker Seeker { get; set; } = null!;
        public Skill Skill { get; set; } = null!;
    }
}