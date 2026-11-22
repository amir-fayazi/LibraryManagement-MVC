using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("Users"); entity.HasKey(x => x.Id);
        entity.Property(x => x.Username).HasMaxLength(30).IsRequired();
        entity.Property(x => x.PasswordHash).HasMaxLength(64).IsRequired();
        entity.Property(x => x.PasswordSalt).HasMaxLength(32).IsRequired();
        entity.Property(x => x.Role).HasConversion<int>().IsRequired();
        entity.Property(x => x.PenaltyAmount).HasPrecision(18, 2);
        entity.HasIndex(x => x.Username).IsUnique();
        SeedData.SeedUsers(entity);
    }
}
