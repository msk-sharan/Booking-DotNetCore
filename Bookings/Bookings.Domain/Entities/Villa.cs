using System.ComponentModel.DataAnnotations;

namespace Bookings.Domain.Entities;

public class Villa
{
    public int Id { get; set; }
    
    [MaxLength(50)]
    public required String Name { get; set; }
    public String? Description { get; set; }
    [Display(Name = "Price Per Night")]
    [Range(10,1000)]
    public double Price { get; set; }
    public int Sqft { get; set; }
    [Range(1,10)]
    public int Occupancy { get; set; }
    [Display(Name = "Image Url")]
    public String? ImageUrl { get; set; }
    public DateTime? Created_Date { get; set; }
    public DateTime? Updated_Date { get; set; }
}