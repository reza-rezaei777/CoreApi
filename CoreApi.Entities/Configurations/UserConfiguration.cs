using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreApi.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p=>p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p=>p.PasswordHash).IsRequired().HasMaxLength(350);
            builder.Property(p => p.FullName).IsRequired();

        }
    }
}
