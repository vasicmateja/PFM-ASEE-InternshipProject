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
       
            if (!Enum.TryParse(transactionKind, out KindEnum kindFilter))
            {
                throw new ArgumentException("Invalid transactionKind value.");
            }

            var query = _db.Transactions.AsQueryable();

            
            query = query.Where(x => x.Kind == kindFilter);


            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                DateTime startDateTime;
                DateTime endDateTime;

                if (DateTime.TryParseExact(startDate, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime)
                    && DateTime.TryParseExact(endDate, "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateTime))
                {
                    endDateTime = endDateTime.AddDays(1); 
                    string formattedStartDate = startDateTime.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
                    string formattedEndDate = endDateTime.ToString("M/d/yyyy", CultureInfo.InvariantCulture);

                    query = query.Where(x => x.Date.CompareTo(formattedStartDate) >= 0 && x.Date.CompareTo(formattedEndDate) < 0);
                }
                else
                {
                    throw new ArgumentException("Invalid date format for startDate and/or endDate. The format should be M/d/yyyy.");
                }
            }


            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount * 1.0 / pageSize);

            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "id":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);
                        break;
                    case "beneficiaryname":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.BeneficiaryName) : query.OrderByDescending(x => x.BeneficiaryName);
                        break;
                    case "description":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description);
                        break;
                    case "amount":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Amount) : query.OrderByDescending(x => x.Amount);
                        break;
                    case "date":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date);
                        break;
                    case "currency":
                        query = sortOrder == SortOrder.asc ? query.OrderBy(x => x.Currency) : query.OrderByDescending(x => x.Currency);
                        break;
                    case "kind":
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
                    HeaderValidated = null, 
                    MissingFieldFound = null 
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
                            existingTransaction.CatCode = ""; 

                            // Sačuvajte promene u bazi podataka
                            _db.SaveChanges();
                        }
                        else
                        {
                            // Dodajte novu transakciju u bazu
                            transaction.Direction = GetDirectionFromString(transaction.Direction);
                            transaction.Amount = Convert.ToDouble(transaction.Amount);
                            transaction.CatCode = ""; 

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

        public async Task CategorizeTransaction(int transactionId)
        {
          
            var transaction =  _db.Transactions.FirstOrDefault(t => t.Id == transactionId);

            if(transaction ==null)
            {
                throw new Exception("Transakcija sa id-em" + transactionId + "ne postoji");
            }

            string[] description = transaction.Description.Split();

            var rulesMapping = new Dictionary<string, string>            
            {
                {"salary", "income"},
                {"supermaket shopping","shopping"},
                {"phone bill", "Bills & Utilities"},
                {"mobile phone bill", "Bills & Utilities"},
                {"internet bill", "Bills & Utilities"}

            };
            /*
             * Rulove za mapiranje bih mozda najpre postavljao unutar nove tabele ili strukute koja bi pamtila i u koju bi učitavali na određeni način tako da se lako menja
            */

            if (transaction.Description.Length < 0)
            {
                return;
            }
            else if (rulesMapping.TryGetValue(transaction.Description.ToLower(),out string catCode))
            {
                transaction.CatCode = _db.Categories.FirstOrDefault(c => c.Name.ToLower().Equals(catCode)).CategoryId;
            }
            else
            {
                List<CategoryEntity> categories = _db.Categories.ToList();

                bool flag = false;

                //Ovde bi bilo potrebno da osmisliti pravila npr kada je description Salary onda da bude income i slicno

                foreach (var category in categories)
                {
                    flag = false;
                    foreach (var word in description)
                    {
                        if (category.Name.Contains(word))
                        {
                            transaction.CatCode = category.CategoryId;
                            flag = true;
                        }
                    }

                    if (flag) { break; }
                }
            }

            _db.SaveChanges();
        }


        public async Task AutoCategorize()
        {
            List<TransactionEntity> transactions = _db.Transactions.ToList();

            foreach (TransactionEntity transaction in transactions)
            {
                CategorizeTransaction(transaction.Id);
            }
            _db.SaveChanges();

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


