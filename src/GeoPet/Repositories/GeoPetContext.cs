using GeoPet.DataContract.Model;
using GeoPet.DataContract.Model.Maps;
using Microsoft.EntityFrameworkCore;

namespace GeoPet.Repository;

public class GeoPetContext : DbContext
{
    public GeoPetContext(DbContextOptions<GeoPetContext> options) : base(options) { }
    public GeoPetContext() { }

    public DbSet<Pet> Pet { get; set; } = null!;
    public DbSet<Owner> Owner { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new PetMap());

        modelBuilder.Entity<Owner>().HasData(
            new Owner
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@email.com",
                Password = "$2b$10$i/dgw2inj3yD4cH0cbvBJ.i6cR/uZqYTua3Ao7wmsk/mytI1opAbS", //Password
                CEP = "01001000"
            },
             new Owner
             {
                 Id = 2,
                 Name = "Pessoa1",
                 Email = "email@email.com",
                 Password = "$2b$10$i/dgw2inj3yD4cH0cbvBJ.i6cR/uZqYTua3Ao7wmsk/mytI1opAbS",
                 CEP = "01001000"
             },
             new Owner
             {
                 Id = 3,
                 Name = "Pessoa2",
                 Email = "email2@email.com",
                 Password = "$2b$10$i/dgw2inj3yD4cH0cbvBJ.i6cR/uZqYTua3Ao7wmsk/mytI1opAbS",
                 CEP = "01001000"
             }
        );

        modelBuilder.Entity<Pet>().HasData(
            new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Pet2",
                Age = 6,
                Size = "pequeno",
                Breed = "Pug",
                OwnerId = 2
            },
            new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Pet3",
                Age = 6,
                Size = "pequeno",
                Breed = "Pug",
                OwnerId = 2
            },
            new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Pet1",
                Age = 5,
                Size = "grande",
                Breed = "Pastor-alemão",
                OwnerId = 3
            }
        );
    }
}