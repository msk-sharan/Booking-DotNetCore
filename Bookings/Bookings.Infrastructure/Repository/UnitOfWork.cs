using Bookings.Application.Common.Interfaces;
using Bookings.Infrastructure.Data;

namespace Bookings.Infrastructure.Repository;

//THis unit of work is done because for each repository we have to add teh services in teh program.cs 
//insted of that we can add all teh repositories here and add this unit of work in the program.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IVillaRepository Villa { get; private set; }
    public IVillaNumberRepository VillaNumber { get; private set; }
    public IAmenityRepository Amenity { get; private set; }

    //We can use this to save all the content because it is comman so that we dont have to do this method in every repository
    public void Save()
    {
        _db.SaveChanges();
    }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Villa = new VillaRepository(_db);
        //It will pass this value to teh base db in the repository
        VillaNumber = new VillaNumberRepository(_db);
        Amenity = new AmenityRepository(_db);
    }
}