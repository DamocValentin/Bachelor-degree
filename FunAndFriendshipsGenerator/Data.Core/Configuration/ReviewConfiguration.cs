using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");

            builder.HasKey(review => review.Id);

            builder
                .HasOne(t => t.User)
                .WithMany(t => t.Reviews)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
               .HasOne(t => t.Activity)
               .WithMany(t => t.Reviews)
               .HasForeignKey(t => t.ActivityId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
