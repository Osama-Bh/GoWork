using System.ComponentModel.DataAnnotations;

namespace GoWork.Models
{
    public class Country
    {
        public int Id { get; set; }              // PK, matches enum numeric value
        public string Name { get; set; } = null!;   // e.g. "Yemen", "Saudi Arabia"
        public string Code { get; set; } = null!;    // ISO code: "YE", "SA"
        public bool IsActive { get; set; } = true;   // soft-delete / enable-disable

        // Navigation: all governates in this country
        public ICollection<Governate> Governates { get; set; } = new List<Governate>();

        // Navigation: all addresses using this country
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }

}
