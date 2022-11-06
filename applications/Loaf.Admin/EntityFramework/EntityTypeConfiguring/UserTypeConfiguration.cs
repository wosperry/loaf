using Loaf.Admin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loaf.Admin.EntityFramework.EntityTypeConfiguring
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            // 索引
            builder.HasIndex(x => x.Account).IsUnique();

            builder.Property(x => x.Name).HasMaxLength(100).HasComment("姓名");
            builder.Property(x => x.Account).HasMaxLength(100).HasComment("账号");
            builder.Property(x => x.Password).HasMaxLength(100).HasComment("密码");
            builder.Property(x => x.Salt).HasMaxLength(100).HasComment("盐");

        }
    }
}
