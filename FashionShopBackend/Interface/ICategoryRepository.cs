using FashionShopBackend.Dto;
using FashionShopBackend.Model;

namespace FashionShopBackend.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> getAllCategory();
        Category getCategoryById(int id);
        void addCategory(CategoryDto category);
        void editCategory(CategoryDto category);
        void deleteCategory(int id);
    }
}
