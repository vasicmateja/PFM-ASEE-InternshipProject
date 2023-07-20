using AutoMapper;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.Mappers
{
    public class AutoMapperProfile : Profile 
    {
        public AutoMapperProfile() {
            CreateMap<TransactionEntity, Transaction>().ForMember( transaction => transaction.TransactionId, entity => entity.MapFrom(x => x.Id));

            CreateMap<Transaction, TransactionEntity>().ForMember(transaction => transaction.Id, entity => entity.MapFrom(x => x.TransactionId));

            CreateMap<TransacitonPageSortedList<TransactionEntity>, TransacitonPageSortedList<Transaction>>();

        }
    }
}
