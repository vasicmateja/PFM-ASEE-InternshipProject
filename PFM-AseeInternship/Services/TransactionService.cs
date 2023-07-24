using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.Services
{
    public interface TransactionService
    {
        Task<PageSortedList<Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, SortOrder sortOrder = SortOrder.asc);

        void ImportTransactions();
    }
}
