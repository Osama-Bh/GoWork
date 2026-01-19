using GoWork.Data;
using GoWork.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoWork.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string AddressLine1 { get; set; } = null!;
        public string? AddressLine2 { get; set; }
        public string PostalCode { get; set; } = null!;
        public int CountryId { get; set; }
        public int GovernateId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; } = null!;
        [ForeignKey("GovernateId")]
        public Governate Governate { get; set; } = null!;
    }

}
