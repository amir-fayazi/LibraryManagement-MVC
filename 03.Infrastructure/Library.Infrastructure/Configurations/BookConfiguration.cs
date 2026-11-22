using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> entity)
    {
        entity.ToTable("Books"); entity.HasKey(x => x.Id);
        entity.Property(x => x.Title).HasMaxLength(150).IsRequired();
        entity.Property(x => x.Author).HasMaxLength(100).IsRequired();
        entity.Property(x => x.Description).HasMaxLength(2000);
        entity.Property(x => x.ImagePath).HasMaxLength(300);
        entity.Property(x => x.Price).HasPrecision(18, 2);
        entity.Property(x => x.CreatedAt).IsRequired();
        entity.HasIndex(x => x.Title).IsUnique();
        entity.HasIndex(x => x.CreatedAt);
        entity.HasOne(x => x.Category).WithMany(x => x.Books).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        SeedData.SeedBooks(entity);
    }
}
