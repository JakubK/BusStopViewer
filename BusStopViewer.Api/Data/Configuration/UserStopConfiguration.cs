using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusStopViewer.Api.Data.Configuration;

public class UserStopConfiguration  : IEntityTypeConfiguration<UserStop>
{
    public void Configure(EntityTypeBuilder<UserStop> builder)
    {
        builder.HasKey(x => new
        {
            x.UserId,
            x.StopId
        });
        
        builder.HasOne(x => x.User)
            .WithMany(y => y.UserStops)
            .HasForeignKey(y => y.UserId);
    }
}