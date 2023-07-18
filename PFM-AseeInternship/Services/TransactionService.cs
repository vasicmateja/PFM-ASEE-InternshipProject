using Microsoft.AspNetCore.Mvc;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.Services
{
    public interface TransactionService
    {
        Task<TransacitonPageSortedList<Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page, int pageSize, string? sortBy, SortOrder sortOrder);
    }
}
