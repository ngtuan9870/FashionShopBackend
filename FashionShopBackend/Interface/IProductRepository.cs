using FashionShopBackend.Dto;
using FashionShopBackend.Model;

namespace FashionShopBackend.Interface
{
    public interface IProductRepository
    {
        ICollection<Product> getAllProduct();
        Product GetProductById(int id);
        void addProduct(ProductDto product);
        void editProduct(ProductDto product);
        void deleteProduct(int id);
    }
}
