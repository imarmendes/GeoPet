using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoPet.DataContract.Model.Maps;

public class PetMap : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name);
        builder.Property(p => p.Age);
        builder.Property(p => p.Size);
        builder.Property(p => p.Breed);
        builder.HasOne(p => p.Owner).WithMany(b => b.Pets);

    }
}
