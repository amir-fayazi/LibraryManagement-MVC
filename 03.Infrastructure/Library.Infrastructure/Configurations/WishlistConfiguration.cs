using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> entity)
    {
        entity.ToTable("Wishlists"); entity.HasKey(x => x.Id);
        entity.HasOne(x => x.Book).WithMany(x => x.Wishlists).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.User).WithMany(x => x.Wishlists).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        entity.HasIndex(x => new { x.UserId, x.BookId }).IsUnique();
    }
}
