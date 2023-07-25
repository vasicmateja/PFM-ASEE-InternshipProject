using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;
using PFM_AseeInternship.Models.CategoryPack;

namespace PFM_AseeInternship.DataBase.Repositories
{
    public interface CategoryRepository
    {
        Task<CategoriesPage<CategoryEntity>> ListCategories(string parentId);

        Task ImportCategories();
    }
}
