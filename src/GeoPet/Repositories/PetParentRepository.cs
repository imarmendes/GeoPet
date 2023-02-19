using GeoPet.DataContract.Model;
using GeoPet.Interfaces.Repository;

namespace GeoPet.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    private readonly GeoPetContext _context;

    public UserRepository(GeoPetContext context) : base(context)
    {
        _context = context;
    }
}