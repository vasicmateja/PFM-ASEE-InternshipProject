using AutoMapper;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;
using PFM_AseeInternship.Models.CategoryPack;

namespace PFM_AseeInternship.Mappers
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() {
            CreateMap<TransactionEntity, Transaction>().ForMember( transaction => transaction.TransactionId, entity => entity.MapFrom(x => x.Id));

            CreateMap<Transaction, TransactionEntity>().ForMember(transaction => transaction.Id, entity => entity.MapFrom(x => x.TransactionId));

            CreateMap<PageSortedList<TransactionEntity>, PageSortedList<Transaction>>();





            CreateMap<CategoryEntity, Category>().ForMember(category => category.CategotyId, entity => entity.MapFrom(x => x.CategoryId));

            CreateMap<Category, CategoryEntity>().ForMember(category => category.CategoryId, entity => entity.MapFrom(x => x.CategotyId));

            CreateMap<CategoriesPage<CategoryEntity>, CategoriesPage<Category>>();

        }
    }
}
