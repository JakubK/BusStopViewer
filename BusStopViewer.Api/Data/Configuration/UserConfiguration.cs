using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusStopViewer.Api.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.UserStops)
            .WithOne(y => y.User)
            .HasForeignKey(y => y.UserId);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
    }
}