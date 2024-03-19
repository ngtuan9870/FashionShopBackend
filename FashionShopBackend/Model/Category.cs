
namespace FashionShopBackend.Model
{
    public class Category
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string category_image { get; set; }
        public string category_description { get; set; }
        public ICollection<Product> Products { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}
