using Microsoft.EntityFrameworkCore;
using PFM_AseeInternship.DataBase.Configruations;
using PFM_AseeInternship.DataBase.Entities;

namespace PFM_AseeInternship.DataBase
{
    public class CategoryDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public CategoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public CategoryDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(
                new CategoryEntityTypeConfiguration());

        }
    }
}
