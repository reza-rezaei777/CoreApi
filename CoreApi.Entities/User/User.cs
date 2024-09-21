using CoreApi.Entities.Domin;
using System.ComponentModel.DataAnnotations;

namespace CoreApi.Entities
{
    public class User : BaseEntity<int>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTimeOffset LastLoginDate { get; set; }
        public ICollection<Post> posts { get; set; }

    }
    public enum GenderType : byte
    {
        [Display(Name = "مرد")]
        Male = 1,
        [Display(Name = "زن")]
        Female = 2
    }
}
