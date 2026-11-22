using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("Categories"); entity.HasKey(x => x.Id);
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.Property(x => x.Description).HasMaxLength(300);
        entity.Property(x => x.ImagePath).HasMaxLength(300);
        entity.HasIndex(x => x.Name).IsUnique();
        SeedData.SeedCategories(entity);
    }
}
