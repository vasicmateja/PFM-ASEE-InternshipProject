using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PFM_AseeInternship.DataBase.Entities;
using PFM_AseeInternship.Models;
using System.Globalization;

namespace PFM_AseeInternship.DataBase.Repositories.Implementation
{
    public class TransactionRepositoryImp : TransactionRepository
    {
        private readonly TransactionDbContext _db;

        public TransactionRepositoryImp(TransactionDbContext db)
        {
            _db = db;
        }

        public async Task<PageSortedList<TransactionEntity>> List(string transactionKind, string? startDate, string? endDate, int page = 1, int pageSize = 10, string? sortBy = null, SortOrder sortOrder = SortOrder.asc)
        {
            // Convert the transactionKind string to the corresponding enum value
            if (!Enum.TryParse(transactionKind, out KindEnum kindFilter))
            {
                throw new ArgumentException("Invalid transactionKind value.");
            }

            var query = _db.Transactions.AsQueryable();

            // Apply filtering based on the transactionKind
            query = query.Where(x => x.Kind == kindFilter);
            


            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount * 1.0 / pageSize);

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
            }
            else
            {
                query = query.OrderBy(x => x.BeneficiaryName);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var transactions = await query.ToListAsync();

            return new PageSortedList<TransactionEntity>
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
    
        public async Task ImportTransactions()
        {
            try
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                    HeaderValidated = null, // Ignore header validation
                    MissingFieldFound = null // Ignore missing fields
                };

                using (var reader = new StreamReader("C:\\Users\\vasic\\source\\repos\\PFM-AseeInternship\\PFM-AseeInternship\\Utils\\transactions.csv"))
                using (var csv = new CsvReader(reader, csvConfig))
                {
                    var transactions = csv.GetRecords<TransactionEntity>().ToList();

                    foreach (var transaction in transactions)
                    {
                        int id = transaction.Id;

                        // Provera da li transakcija već postoji u bazi
                        var existingTransaction = _db.Transactions.FirstOrDefault(t => t.Id == id);
                        if (existingTransaction != null)
                        {
                            // Ažurirajte potrebne vrednosti postojeće transakcije
                            existingTransaction.Direction = GetDirectionFromString(transaction.Direction);
                            existingTransaction.Amount = Convert.ToDouble(transaction.Amount);
                            existingTransaction.CatCode = ""; // Možete promeniti vrednost prema potrebi

                            // Sačuvajte promene u bazi podataka
                            _db.SaveChanges();
                        }
                        else
                        {
                            // Dodajte novu transakciju u bazu
                            transaction.Direction = GetDirectionFromString(transaction.Direction);
                            transaction.Amount = Convert.ToDouble(transaction.Amount);
                            transaction.CatCode = ""; // Možete promeniti vrednost prema potrebi

                            _db.Transactions.Add(transaction);
                            _db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška pri ubacivanju transakcija u bazu: " + ex.Message);
            }
        }

        public Task CategorizeTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }



        private Directions GetDirectionFromString(Directions directionEnum)
        {

            string direction = directionEnum.ToString().ToLower();   
            if (direction.Equals("c"))
            {
                return Directions.c;
            }
            else if (direction.Equals("d"))
            {
                return Directions.d;
            }
            else
            {
                return Directions.unknown;
            }
        }
    }
}


