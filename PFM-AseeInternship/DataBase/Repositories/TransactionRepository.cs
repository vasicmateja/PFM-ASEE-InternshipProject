﻿
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.DataBase.Repositories
{
    public interface TransactionRepository
    {
        Task<PageSortedList<TransactionEntity>> List(string transactionKind, string? startDate, string? endDate, int page = 1
                                                                            , int pageSize = 10, string? sortBy = null, SortOrder sortOrder = SortOrder.asc);

        Task ImportTransactions();

        Task CategorizeTransaction(int transactionId);

        Task AutoCategorize();
    }
}
