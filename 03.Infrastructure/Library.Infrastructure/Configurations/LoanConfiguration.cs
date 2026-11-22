using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class LoanConfiguration : IEntityTypeConfiguration<BookLoan>
{
    public void Configure(EntityTypeBuilder<BookLoan> entity)
    {
        entity.ToTable("BookLoans"); entity.HasKey(x => x.Id);
        entity.Property(x => x.FineAmount).HasPrecision(18, 2);
        entity.HasOne(x => x.Book).WithMany(x => x.Loans).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.User).WithMany(x => x.Loans).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        entity.HasIndex(x => x.BookId).IsUnique().HasFilter("[IsReturned] = 0");
        entity.HasIndex(x => new { x.UserId, x.IsReturned });
    }
}
