using GeoPet.DataContract.Model;

namespace GeoPet.Interfaces.Repository;

public interface IOwnerRepository : IRepositoryBase<Owner>
{
    Task<Owner> GetByEmail(string email);
}