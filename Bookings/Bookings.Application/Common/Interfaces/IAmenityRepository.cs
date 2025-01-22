using Bookings.Domain.Entities;

namespace Bookings.Application.Common.Interfaces;

public interface IAmenityRepository:IRepository<Amenity>
{
    void Update(Amenity entity);
}