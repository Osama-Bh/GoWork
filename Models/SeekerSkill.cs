namespace GoWork.Models
{
    public class SeekerSkill
    {
        public int SeekerId { get; set; }
        public int SkillId { get; set; }

        // Navigation
        public Seeker Seeker { get; set; }
        public Skill Skill { get; set; }
    }
}