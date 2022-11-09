using Loaf.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loaf.Admin.Entities
{
    public class Tttttttttttt : Entity<Guid>
    {
        public int Count { get; set; } 
    }
    public class TtttttttttttIEntityTypeConfiguration : Entity<Guid>, IEntityTypeConfiguration<Tttttttttttt>
    {
        public int Count { get; set; }

        public void Configure(EntityTypeBuilder<Tttttttttttt> builder)
        {
            builder.ToTable("ttt");
            builder.Property(x => x.Count).HasColumnName("count");
        }
    }
}
