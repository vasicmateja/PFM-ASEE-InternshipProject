using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.Services.Implementations
{
    public class TransactionServiceImplementation : TransactionService
    {
        public TransactionServiceImplementation() { }
        public Task<TransacitonPageSortedList<Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page, int pageSize, string? sortBy, SortOrder sortOrder)
        {
            throw new NotImplementedException();
        }
    }
}
