namespace FashionShopBackend.Model
{
    public class Product
    {
        public int product_id { get; set; }
        public string product_name { get; set; }
        public string product_image { get; set; }
        public string product_description { get; set; }
        public double product_price { get; set; }
        public int product_promotion { get; set; }
        public int category_id { get; set; }
        public Category Category { get; set; }
    }
}
