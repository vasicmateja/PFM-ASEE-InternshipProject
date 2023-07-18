using Microsoft.EntityFrameworkCore;
using PFM_AseeInternship.DataBase.Configruations;
using PFM_AseeInternship.DataBase.Entities;

namespace PFM_AseeInternship.DataBase
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }
        public TransactionDbContext(DbContextOptions options): base(options)
        { 
        }

        protected TransactionDbContext() {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            modelBuilder.ApplyConfiguration(
                new TransactionEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
