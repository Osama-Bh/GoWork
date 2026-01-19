using GoWork.Data;
using GoWork.Models;

public class Address
{
    public int Id { get; set; }

    // Owner user (covers seeker/employer/person)
    public int? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    // Optional specific job this address is for
    public int? JobId { get; set; }
    public Job? Job { get; set; }

    public string AddressLine1 { get; set; } = null!;
    public string? AddressLine2 { get; set; }
    public string PostalCode { get; set; } = null!;
    public int CountryId { get; set; }
    public int GovernateId { get; set; }

    public Country Country { get; set; } = null!;
    public Governate Governate { get; set; } = null!;
}
