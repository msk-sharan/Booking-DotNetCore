using Bookings.Application.Common.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Data;

namespace Bookings.Infrastructure.Repository;

public class AmenityRepository:Repository<Amenity>,IAmenityRepository
{
    private readonly ApplicationDbContext _db;
    
    public AmenityRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Amenity entity)
    {
        _db.Amenities.Update(entity);
    }
}