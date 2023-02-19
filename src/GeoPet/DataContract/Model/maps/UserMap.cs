using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeoPet.DataContract.Model.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        builder.Property(u => u.Name);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Password);
        builder.Property(u => u.CEP);
        builder.HasMany(u => u.Pets).WithOne();
    }
}
