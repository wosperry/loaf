using Loaf.EntityFrameworkCore.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loaf.EntityFrameworkCore.Tests.EntityTypeConfigurations
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("bus_student");

            builder.Property(x => x.Name).HasMaxLength(100).HasComment("name");
            builder.Property(x => x.NickName).HasMaxLength(50).HasComment("nickname");
            builder.Property(x => x.Birthday).HasComment("birthday");
        }
    }
}