using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM_AseeInternship.DataBase.Entities;

namespace PFM_AseeInternship.DataBase.Configruations
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public TransactionEntityTypeConfiguration() { }

        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
           // builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.BeneficiaryName);
            builder.Property(x =>x.Date).IsRequired();
            builder.Property(x => x.Direction).IsRequired();
            //builder.Property(x => x.Direction).HasConversion<string>.IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Currency).IsRequired().HasMaxLength(3); // kako stavljam ogranicenje za minLength
            //builder.Property(x => x.MccCode).HasConversion<string>;
            builder.Property(x => x.MccCode);
            //builder.Property(x => x.Kind).HasConversion<string>.IsRequired();
            builder.Property(x => x.Kind).IsRequired();
            builder.Property(x => x.CatCode);
            builder.Property(x => x.Splits);



        }
    }
}
