using Microsoft.EntityFrameworkCore;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.DataBase.Repositories.Implementation
{
    public class TransactionRepositoryImp : TransactionRepository
    {
        TransactionDbContext _db;

        public TransactionDbContext Db { get { return _db; } }

        public async Task<TransacitonPageSortedList<TransactionEntity>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, SortOrder sortOrder = SortOrder.asc)
        {
            var query = _db.Transactions.AsQueryable();
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / pageSize * 1.0);

            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "id":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                        break;
                    case "BeneficiaryName":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.BeneficiaryName) : query.OrderByDescending(x => x.BeneficiaryName);
                        break;
                    case "Description":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description);
                        break;
                    case "Amount":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Amount) : query.OrderByDescending(x => x.Amount);
                        break;
                    case "Date":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date);
                        break;
                    case "Currency":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Currency) : query.OrderByDescending(x => x.Currency);
                        break;
                    case "Kind":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Kind) : query.OrderByDescending(x => x.Kind);
                        break;
                }

            }else {
                    query = query.OrderBy(x => x.BeneficiaryName);
            }
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var transactions = await query.ToListAsync();

            return new TransacitonPageSortedList<TransactionEntity>
            {
                TotalPages = totalPages,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,    
                SortBy = sortBy,
                SortOrder = sortOrder,
                Items = transactions

            };
        }
    }
}
