using Bookings.Application.Common.Interfaces;
using Bookings.Domain.Entities;
using Bookings.Infrastructure.Data;

namespace Bookings.Infrastructure.Repository;

public class VillaNumberRepository:Repository<VillaNumber>,IVillaNumberRepository
{
    private readonly ApplicationDbContext _db;
    
    public VillaNumberRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(VillaNumber entity)
    {
        _db.VillaNumberss.Update(entity);
    }
}