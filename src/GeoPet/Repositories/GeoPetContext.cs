using GeoPet.DataContract.Model;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Repository;

public class GeoPetContext : DbContext
{
    public GeoPetContext(DbContextOptions<GeoPetContext> options) : base(options) { }
    public GeoPetContext() { }
    
    public DbSet<PetDto> PetDtos { get; set; }
    public DbSet<PetParentDto> PetParentDtos { get; set; }
}