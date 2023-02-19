using GeoPet.DataContract.Model;
using GeoPet.DataContract.Model.Maps;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Repository;

public class GeoPetContext : DbContext
{
    public GeoPetContext(DbContextOptions<GeoPetContext> options) : base(options) { }
    public GeoPetContext() { }

    public DbSet<Pet> Pet { get; set; } = null!;
    public DbSet<User> User { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new PetMap());
    }
}