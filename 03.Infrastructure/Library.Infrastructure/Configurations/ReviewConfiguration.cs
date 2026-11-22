using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> entity)
    {
        entity.ToTable("Reviews"); entity.HasKey(x => x.Id);
        entity.Property(x => x.Comment).HasMaxLength(1000);
        entity.Property(x => x.Status).HasConversion<int>();
        entity.HasOne(x => x.Book).WithMany(x => x.Reviews).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.User).WithMany(x => x.Reviews).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        entity.HasIndex(x => new { x.UserId, x.BookId }).IsUnique();
    }
}
