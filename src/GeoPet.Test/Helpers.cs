using Microsoft.EntityFrameworkCore;
using GeoPet.DataContract.Model;
using GeoPet.Repository;


namespace GeoPet.Test;

public static class Helpers
{

    public static void ResetDbForTests(GeoPetContext db)
    {
        db.Owner.RemoveRange(db.Owner);
        db.Pet.RemoveRange(db.Pet);

        db.Owner.AddRange(GetOwnerList());
        db.Pet.AddRange(GetPetList());

        db.SaveChanges();
    }

    public static List<Owner> GetOwnerList()
    {
        return new() {
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
        };
    }

    public static List<Pet> GetPetList()
    {
        return new() {
            new Pet
            {
                Id = Guid.Parse("08148B33-D29C-41B8-B797-EC903D9EDE71"),
                Name = "Pet2",
                Age = 6,
                Size = "pequeno",
                Breed = "Pug",
                OwnerId = 2
            },
            new Pet
            {
                Id = Guid.Parse("E646F236-E19D-4C3A-99B9-1FD09E21EB4F"),
                Name = "Pet3",
                Age = 6,
                Size = "pequeno",
                Breed = "Pug",
                OwnerId = 2
            },
            new Pet
            {
                Id = Guid.Parse("D386421C-B9DD-46F0-8EC1-5D788C1A33A6"),
                Name = "Pet1",
                Age = 5,
                Size = "grande",
                Breed = "Pastor-alemão",
                OwnerId = 3
            }
        };
    }
}

