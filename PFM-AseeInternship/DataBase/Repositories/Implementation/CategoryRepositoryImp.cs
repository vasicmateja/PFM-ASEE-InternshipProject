using Microsoft.EntityFrameworkCore;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;
using PFM_AseeInternship.Models.CategoryPack;

namespace PFM_AseeInternship.DataBase.Repositories.Implementation
{
    public class CategoryRepositoryImp : CategoryRepository
    {

        private readonly CategoryDbContext _db;

        public CategoryRepositoryImp(CategoryDbContext db)
        {
            _db = db;
        }


        public void ImportCategories()
        {
            CategoryEntity category = new CategoryEntity();

            using (var reader = new StreamReader("C:\\Users\\vasic\\source\\repos\\PFM-AseeInternship\\PFM-AseeInternship\\Utils\\categories.csv"))
            {
                int a = -1;

                while (!reader.EndOfStream)
                {
                    a++;
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (a == 0) continue;
                 
                    category.CategoryId = int.Parse(values[0]);
                    category.Name = values[1];
                    category.ParentCode = values[2];

                    var query = _db.Categories.Add(category);

                }

            }

    }

        public async Task<CategoriesPage<CategoryEntity>> ListCategories(string parentId)
        {
            var query = _db.Categories.AsQueryable();
            var totalCount = query.Count();
            
            var categories = await query.ToListAsync();

            return new CategoriesPage<CategoryEntity>
            {
                Items = categories
            };
        }
    }
}
