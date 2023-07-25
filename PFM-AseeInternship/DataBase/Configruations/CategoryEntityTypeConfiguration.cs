using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM_AseeInternship.DataBase.Entities;

namespace PFM_AseeInternship.DataBase.Configruations 
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public CategoryEntityTypeConfiguration() { }

        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Categories");

            builder.HasKey(x => x.CategoryId);

            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ParentCode);
        }
    }
}
