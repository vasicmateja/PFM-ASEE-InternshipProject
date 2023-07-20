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

        public TransactionDbContext() {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //modelBuilder.Entity<TransactionEntity>().Ignore(t => t.Splits);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(
                new TransactionEntityTypeConfiguration());
            
        }
    }
}
