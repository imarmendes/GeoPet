using GeoPet.DataContract.Model;
using GeoPet.Interfaces.Repository;

namespace GeoPet.Repository;

public class PetRepository : RepositoryBase<Pet>, IPetRepository
{
    private readonly GeoPetContext _context;

    public PetRepository(GeoPetContext context) : base(context)
    {
        _context = context;
    }
}