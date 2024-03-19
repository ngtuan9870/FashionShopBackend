using FashionShopBackend.Dto;
using FashionShopBackend.Interface;
using FashionShopBackend.Model;
using FashionShopNETCoreAPI.Data;

namespace FashionShopBackend.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly FashionShopDBContext _context;
        public CategoryRepository(FashionShopDBContext context)
        {
            _context = context;
        }
        public void addCategory(CategoryDto category)
        {
            var c = new Category
            {
                category_name = category.category_name,
                category_image = category.category_image,
                category_description = category.category_description,
            };
            _context.Add(c);
            _context.SaveChanges();
        }

        public void deleteCategory(int id)
        {
            var category_delete = _context.Categories.SingleOrDefault(c => c.category_id == id);
            _context.Categories.Remove(category_delete);
            _context.SaveChanges();
        }

        public void editCategory(CategoryDto category)
        {
            var category_edit = _context.Categories.SingleOrDefault(c => c.category_id == category.category_id);
            category_edit.category_name = category.category_name;
            category_edit.category_image = category.category_image;
            category_edit.category_description = category.category_description;
            _context.SaveChanges();
        }

        public ICollection<Category> getAllCategory()
        {
            return _context.Categories.OrderBy(c => c.category_id).ToList();
        }

        public Category getCategoryById(int id)
        {
            return _context.Categories.Where(c => c.category_id == id).FirstOrDefault();
        }
    }
}
