namespace FashionShopNETCoreAPI.Model
{
    public class CategoryVM
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string category_image { get; set; }
        public string category_description { get; set; }
        public ICollection<ProductVM> Products { get; set; }
    }
}
