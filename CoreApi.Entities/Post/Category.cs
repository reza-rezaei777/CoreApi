using CoreApi.Entities.Domin;

namespace CoreApi.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentCategoryID { get; set; }
        public Category ParentCategory { get; set; }
        public ICollection<Category> ChildCategories { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
