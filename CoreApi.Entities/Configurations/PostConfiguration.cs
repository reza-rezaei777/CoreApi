using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreApi.Entities.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p=>p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p=>p.Description).IsRequired().HasMaxLength(350);
            builder.HasOne(p=>p.User).WithMany(p=>p.posts).HasForeignKey(p=>p.AuthorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p=>p.Category).WithMany(p=>p.Posts).HasForeignKey(p=>p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
