using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activity");

            builder.HasKey(activity => activity.Id);
            builder.Property(activity => activity.StartTime).IsRequired();
            builder.Property(activity => activity.EndTime).IsRequired();
            builder.Property(activity => activity.Cost).IsRequired();
            builder.Property(activity => activity.ParticipantsNumber).IsRequired();
            builder.Property(activity => activity.Description).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
            builder.Property(activity => activity.Name).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
            builder.Property(activity => activity.Location).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();

            builder
               .HasOne(t => t.ActivityType)
               .WithMany(t => t.Activities)
               .HasForeignKey(t => t.ActivityTypeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
