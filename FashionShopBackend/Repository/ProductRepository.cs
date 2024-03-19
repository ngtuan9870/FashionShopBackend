using FashionShopBackend.Dto;
using FashionShopBackend.Interface;
using FashionShopBackend.Model;
using FashionShopNETCoreAPI.Data;

namespace FashionShopBackend.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly FashionShopDBContext _context;
        public ProductRepository(FashionShopDBContext context)
        {
            _context = context;
        }

        public void addProduct(ProductDto product)
        {
            var p= new Product
            {
                product_name = product.product_name,
                product_image = product.product_image,
                product_description = product.product_description,
                product_price = product.product_price,
                category_id = product.category_id
            };
            _context.Add(p);
            _context.SaveChanges();
        }

        public void deleteProduct(int id)
        {
            var product_delete = _context.Products.SingleOrDefault(p => p.product_id == id);
            _context.Products.Remove(product_delete);
            _context.SaveChanges();
        }

        public void editProduct(ProductDto product)
        {
            //LINQ [Object] Query
            var product_edit = _context.Products.SingleOrDefault(p => p.product_id == product.product_id);
            product_edit.product_name = product.product_name;
            product_edit.product_image = product.product_image;
            product_edit.product_description = product.product_description;
            product_edit.product_price = product.product_price;
            product_edit.product_promotion = product.product_promotion;
            _context.SaveChanges();
        }

        public ICollection<Product> getAllProduct() {
            return _context.Products.OrderBy(p => p.product_id).ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.product_id == id).FirstOrDefault();
        }
    }
}
