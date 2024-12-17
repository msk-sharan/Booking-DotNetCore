namespace Bookings.Domain.Entities;

public class Villa
{
    public int Id { get; set; }
    public required String Name { get; set; }
    public String? Description { get; set; }
    public double Price { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public String? ImageUrl { get; set; }
    public DateTime? Created_Date { get; set; }
    public DateTime? Updated_Date { get; set; }
}