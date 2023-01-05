using Loaf.EntityFrameworkCore.Tests.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loaf.EntityFrameworkCore.Tests.EFCore.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasIndex(x => x.UserName);

            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(128).IsRequired();
        }
    }
}
