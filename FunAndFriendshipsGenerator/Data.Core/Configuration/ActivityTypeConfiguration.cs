using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.ToTable("ActivityType");
            builder.HasKey(activityType => activityType.Id);
            builder.Property(activityType => activityType.ActivityTypeName).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
        }
    }
}
