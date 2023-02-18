using GeoPet.DataContract.Model;
using GeoPet.Interfaces.Repository;

namespace GeoPet.Repository;

public class PetParentRepository : RepositoryBase<PetParentDto>, IPetParentRepository
{
    private readonly GeoPetContext _context;

    public PetParentRepository(GeoPetContext context) : base(context)
    {
        _context = context;
    }
}