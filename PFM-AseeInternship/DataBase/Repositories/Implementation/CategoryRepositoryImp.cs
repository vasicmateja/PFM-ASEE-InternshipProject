using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;
using PFM_AseeInternship.Models.CategoryPack;
using System.Globalization;

namespace PFM_AseeInternship.DataBase.Repositories.Implementation
{
    public class CategoryRepositoryImp : CategoryRepository
    {

        private readonly TransactionDbContext _db;

        public CategoryRepositoryImp(TransactionDbContext db)
        {
            _db = db;
        }


        public async Task ImportCategories()
        {

            try
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                    HeaderValidated = null, // Ignore header validation
                    MissingFieldFound = null // Ignore missing fields
                };

                using (var reader = new StreamReader("C:\\Users\\vasic\\source\\repos\\PFM-AseeInternship\\PFM-AseeInternship\\Utils\\categories.csv"))
                using (var csv = new CsvReader(reader, csvConfig))
                {
                    var categories = csv.GetRecords<CategoryEntity>().ToList();

                    foreach (var category in categories)
                    {
                        string id = category.CategoryId;
                        // Provera da li transakcija već postoji u bazi
                        /*
                        var existingCategory = _db.Categories.FirstOrDefault(t => t.CategoryId.Equals(id));
                        if (existingCategory != null)
                            continue; // Preskočite upis, transakcija već postoji

                        */

                        
                        category.ParentCode = category.ParentCode;
                        category.Name = category.Name;

                        // Dodajte transakciju u bazu
                        _db.Categories.Add(category);
                        _db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Greška pri ubacivanju kategorije u bazu: " + ex.Message);
            }
        }


        public async Task<CategoriesPage<CategoryEntity>> ListCategories(string parentId)
        {
            var query = _db.Categories.AsQueryable();
            var totalCount = query.Count();


            
            query = query.Where(x => x.ParentCode.Equals(parentId));


            var categories = await query.ToListAsync();

            return new CategoriesPage<CategoryEntity>
            {
                Items = categories
            };
        }
    }
}
