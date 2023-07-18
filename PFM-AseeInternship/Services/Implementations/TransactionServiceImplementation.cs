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
        public async Task<TransacitonPageSortedList<Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page, int pageSize, string? sortBy, SortOrder sortOrder)
        {
            var transactions = await _transactionRepository.GetTransactions(transactionKind, startDate, endDate, page, pageSize, sortBy, sortOrder);
            return _mapper.Map<TransacitonPageSortedList<Transaction>>(transactions);
        }
    }
}
