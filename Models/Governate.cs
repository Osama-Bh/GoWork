using GoWork.Models;

public class Governate
{
    public int Id { get; set; }          // e.g., 1 = Aden
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;

    public string Name { get; set; } = null!;

    public ICollection<Address> Addresses { get; set; } = new List<Address>();
}