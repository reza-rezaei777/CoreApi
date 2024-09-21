using CoreApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CoreApi.Entities.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p=>p.Desciption).IsRequired().HasMaxLength(150);
        }
    }
}
