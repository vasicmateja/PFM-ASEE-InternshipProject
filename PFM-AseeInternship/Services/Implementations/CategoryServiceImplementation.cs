using AutoMapper;
using PFM_AseeInternship.DataBase.Repositories;
using PFM_AseeInternship.Models;
using PFM_AseeInternship.Models.CategoryPack;

namespace PFM_AseeInternship.Services.Implementations
{
    public class CategoryServiceImplementation : CategoryService
    {
        CategoryRepository _categoryRepository;
        IMapper _mapper;

        public CategoryServiceImplementation(CategoryRepository transactionRepository, IMapper mapper)
        {
            _categoryRepository = transactionRepository;
            _mapper = mapper;
        }
        public async Task<CategoriesPage<Category>> GetCategories(string parentId)
        {
            var result = await _categoryRepository.ListCategories(parentId);
            return _mapper.Map<CategoriesPage<Category>>(result);
        }


        public void ImportCategories()
        {
            _categoryRepository.ImportCategories();
        }

        
    }
}
