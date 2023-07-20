using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PFM_AseeInternship.DataBase.Repositories;
using PFM_AseeInternship.Models;
using System.Globalization;

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

        public Task<TransacitonPageSortedList<Transaction>> GetTransactions(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, Models.SortOrder sortOrder = Models.SortOrder.asc)
        {
            throw new NotImplementedException();
        }

        public void ImportTransactions()
        {
            throw new NotImplementedException();
        }
    }
}
