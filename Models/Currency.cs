using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class Currency
    {
        public int Id { get; set; }              // PK, matches enum numeric value
        public string Code { get; set; } = null!;   // e.g. "USD", "YER", "SAR"
        public string Name { get; set; } = null!;    // e.g. "US Dollar", "Yemeni Rial"
        public bool IsActive { get; set; } = true;   // soft-delete / enable-disable

        // Navigation: all jobs/salaries using this currency
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }

}