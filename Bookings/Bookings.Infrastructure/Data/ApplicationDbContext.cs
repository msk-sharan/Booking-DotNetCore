using Bookings.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookings.Infrastructure.Data;


//Creating a application db context by implementing the dbcontext interface
public class ApplicationDbContext : DbContext
{
    //Making application db context as a option and make it as a base option
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    //When creating a model make it a db set so that a table can be created in the database
    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumberss { get; set; }
    
    public DbSet<Amenity> Amenities { get; set; }

   

}