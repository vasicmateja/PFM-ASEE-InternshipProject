using AutoMapper;
using PFM_AseeInternship.DataBase.Repositories;
using PFM_AseeInternship.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PFM_AseeInternship.Services.Implementations
{
    public class TransactionServiceImplementation : TransactionService
    {
        TransactionRepository _transactionRepository;
        IMapper _mapper;

        public TransactionServiceImplementation(TransactionRepository transactionRepository, IMapper mapper) {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
    }

        public async Task<TransacitonPageSortedList<Models.Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, Models.SortOrder sortOrder = Models.SortOrder.asc)
        {
            var result = await _transactionRepository.List(transactionKind, startDate, endDate, page, pageSize, sortBy, sortOrder);  

            return _mapper.Map<TransacitonPageSortedList<Models.Transaction>>(result);
        }

        public async void ImportTransactions()
        {
             _transactionRepository.ImportTransactions();
        }


    }
}
