using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.ToTable("UserActivity");

            builder.HasKey(userActivity => userActivity.Id);

            builder
                .HasOne(t => t.User)
                .WithMany(t => t.UserActivities)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(t => t.Activity)
               .WithMany(t => t.UserActivities)
               .HasForeignKey(t => t.ActivityId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
