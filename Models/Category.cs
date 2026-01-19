using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 100 characters")]
        public string Name { get; set; } = null!;
        public ICollection<Job>? Jobs { get; set; }
    }
}
