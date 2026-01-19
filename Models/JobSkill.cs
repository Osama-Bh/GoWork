using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class JobSkill
    {
        [Key]
        public int JobId { get; set; }
        public int SkillId { get; set; }

        // Navigation properties
        public Job Job { get; set; }
        public Skill Skill { get; set; }
    }
}