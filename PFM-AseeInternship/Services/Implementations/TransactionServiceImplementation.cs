using AutoMapper;
using PFM_AseeInternship.DataBase.Repositories;
using PFM_AseeInternship.Models;

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

     

        public async Task<PageSortedList<Models.Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, Models.SortOrder sortOrder = Models.SortOrder.asc)
        {
            var result = await _transactionRepository.List(transactionKind, startDate, endDate, page, pageSize, sortBy, sortOrder);  

            return _mapper.Map<PageSortedList<Models.Transaction>>(result);
        }

        public async void ImportTransactions()
        {
             _transactionRepository.ImportTransactions();
        }

        public void CategorizeTransaction(int transactionId)
        {
            _transactionRepository.CategorizeTransaction(transactionId);
        }

        public void AutoCategorize()
        {
            _transactionRepository.AutoCategorize();
        }
    }
}
