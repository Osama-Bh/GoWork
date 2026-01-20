using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Skill Name is required.")]
        [StringLength(70, ErrorMessage = "Skill Name cannot exceed 70 characters.")]
        public string Name { get; set; } = null!;

        public ICollection<JobSkill>? JobSkills { get; set; }
        public ICollection<SeekerSkill>? SeekerSkills { get; set; }
    }

}
