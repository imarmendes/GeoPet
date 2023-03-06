using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoPet.DataContract.Model.Maps;

public class UserMap : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Password);
        builder.Property(u => u.CEP);
        builder.HasMany(u => u.Pets).WithOne();
    }
}
