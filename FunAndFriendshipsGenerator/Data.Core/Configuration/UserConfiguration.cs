using Constants;
using Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Core.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
            builder.Property(user => user.PasswordHash).HasMaxLength(ConfigurationConstants.MaxLength).IsRequired();
        }
    }
}