using CoreApi.Entities.Domin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CoreApi.Entities
{
    public class User : IdentityUser<int>,IEntity
    {
        public User()
        {
            IsActive= true;
        }
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset LastLoginDate { get; set; }
        public ICollection<Post> posts { get; set; }

    }
    public class UserConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p=>p.UserName).IsRequired().HasMaxLength(128);
        }
    }
    public enum GenderType : byte
    {
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }
}
