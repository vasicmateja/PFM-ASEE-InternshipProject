using PFM_AseeInternship.Models;
using PFM_AseeInternship.Models.CategoryPack;

namespace PFM_AseeInternship.Services
{
    public interface CategoryService
    {
        Task<CategoriesPage<Category>> GetCategories(string parentId);

        void ImportCategories();
    }
}
