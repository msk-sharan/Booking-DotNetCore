using Bookings.Domain.Entities;

namespace Bookings.Application.Common.Interfaces;

public interface IVillaNumberRepository: IRepository<VillaNumber>
{
    void Update(VillaNumber entity);

}