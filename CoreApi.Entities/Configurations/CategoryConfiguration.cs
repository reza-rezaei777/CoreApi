using CoreApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreApi.Entities.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(150);
            builder.HasOne(p=>p.ParentCategory).WithMany(p=>p.ChildCategories).HasForeignKey(p=>p.ParentCategoryID).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
