using Microsoft.EntityFrameworkCore;
using Npgsql;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;

namespace PFM_AseeInternship.DataBase.Repositories.Implementation
{
    public class TransactionRepositoryImp : TransactionRepository
    {
        private readonly TransactionDbContext _db;

        public TransactionRepositoryImp(TransactionDbContext db)
        {
            _db = db;
        }

        public async Task<TransacitonPageSortedList<TransactionEntity>> List(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, SortOrder sortOrder = SortOrder.asc)
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

        public async void ImportTransactions()
        {

            TransactionEntity transaction = new TransactionEntity();

            using (var reader = new StreamReader("C:\\Users\\vasic\\source\\repos\\PFM-AseeInternship\\PFM-AseeInternship\\Utils\\transactions.csv"))
            {
                int a = -1;

                while (!reader.EndOfStream)
                {
                    a++;
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (a == 0)
                    {
                        //OVDE MOZEMO HANDLOVATI PROVERU ZA FAJL
                        continue;
                    }
                    

                    transaction.Id = int.Parse(values[0]);
                    transaction.BeneficiaryName = values[1];
                    transaction.Date = values[2];
                    if (values[3].Equals("c"))
                    {
                        transaction.Direction = Directions.c;
                    }
                    else if (values[3].Equals("d"))
                    {
                        transaction.Direction = Directions.d;
                    }
                    else
                    {
                        //TODO GRESKA 
                    }

                    if (int.TryParse(values[4], out int amount))
                    {
                        transaction.Amount = amount * 1.0;
                    }
                    else
                    {
                        // Greska pri parsiranju amount
                    }

                    transaction.Description = values[5];
                    transaction.Currency = values[6];
                    transaction.MccCode = values[7];
                    if (Enum.IsDefined(typeof(KindEnum), values[8]))
                    {
                        transaction.Kind = Enum.Parse<KindEnum>(values[8]);
                    }
                    else
                    {
                        // Neispravna vrednost za KindEnum
                        // Postupiti u skladu sa potrebama aplikacije (npr. postaviti podrazumevanu vrednost, baciti izuzetak, itd.)
                    }


                    var query = _db.Transactions.Add(transaction);
                    
                }
                // _db.SaveChangesAsync();
            }
            
        }
    }
}
