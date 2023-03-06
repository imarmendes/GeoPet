using GeoPet.DataContract.Model;
using GeoPet.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Repository;

public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
{
    private readonly GeoPetContext _context;

    public OwnerRepository(GeoPetContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Owner> GetByEmail(string email)
    {
        try
        {
            var owner = await _context.Owner.AsNoTracking().SingleAsync(o => o.Email == email);

            return owner;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Erro ao tentar buscar por email.");
        }
    }
}