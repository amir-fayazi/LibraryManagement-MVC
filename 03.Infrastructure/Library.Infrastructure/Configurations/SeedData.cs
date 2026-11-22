using Library.Domain.Entities;
using Library.Domain.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Library.Infrastructure.Configurations;
public static class SeedData
{
    public static void SeedCategories(EntityTypeBuilder<Category> entity)
    {
        entity.HasData(new { Id = 1, Name = "رمان و داستان", Description = "معاصر و کلاسیک", ImagePath = "/images/categories/fiction.svg" });
        entity.HasData(new { Id = 2, Name = "فلسفه و منطق", Description = "غرب و شرق", ImagePath = "/images/categories/philosophy.svg" });
        entity.HasData(new { Id = 3, Name = "علمی و تاریخی", Description = "فناوری و تاریخ", ImagePath = "/images/categories/science.svg" });
        entity.HasData(new { Id = 4, Name = "کودک و نوجوان", Description = "داستان‌های مصور", ImagePath = "/images/categories/children.svg" });
        entity.HasData(new { Id = 5, Name = "هنر و معماری", Description = "طراحی و نقاشی", ImagePath = "/images/categories/art.svg" });
    }
    public static void SeedBooks(EntityTypeBuilder<Book> entity)
    {
        entity.HasData(new { Id = 1, Title = "آخرین نفس", Author = "آنتونیو مارکز", CategoryId = 1, Price = 490000m, PageCount = 400, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/last-breath.jpg", CreatedAt = new DateTime(2026, 11, 21, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 2, Title = "سفر به مریخ", Author = "لورا هریس", CategoryId = 3, Price = 620000m, PageCount = 280, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/journey-to-mars.jpg", CreatedAt = new DateTime(2026, 11, 20, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 3, Title = "اصول رهبری مدرن", Author = "دکتر جان اسمیت", CategoryId = 2, Price = 750000m, PageCount = 310, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/modern-leadership.jpg", CreatedAt = new DateTime(2026, 11, 19, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 4, Title = "حکمت‌های کهن", Author = "ماروسیو کانت", CategoryId = 2, Price = 380000m, PageCount = 550, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/ancient-wisdom.jpg", CreatedAt = new DateTime(2026, 11, 18, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 5, Title = "تغییرات قرن بیستم", Author = "دایان وایلد", CategoryId = 3, Price = 550000m, PageCount = 440, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/twentieth-century-changes.jpg", CreatedAt = new DateTime(2026, 11, 17, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 6, Title = "پشت پنجره باران", Author = "نیلوفر شریفی", CategoryId = 1, Price = 320000m, PageCount = 212, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-06.svg", CreatedAt = new DateTime(2026, 10, 06, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 7, Title = "هنر ساده زیستن", Author = "سارا رضایی", CategoryId = 2, Price = 210000m, PageCount = 180, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-07.svg", CreatedAt = new DateTime(2026, 10, 07, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 8, Title = "تاریخ کوتاه جهان", Author = "رضا محسنی", CategoryId = 3, Price = 460000m, PageCount = 364, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-08.svg", CreatedAt = new DateTime(2026, 10, 08, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 9, Title = "شهر کاغذی", Author = "پریسا احمدی", CategoryId = 1, Price = 290000m, PageCount = 260, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-09.svg", CreatedAt = new DateTime(2026, 10, 09, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 10, Title = "ستاره‌های کوچک", Author = "مینا رضایی", CategoryId = 4, Price = 175000m, PageCount = 120, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-10.svg", CreatedAt = new DateTime(2026, 10, 10, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 11, Title = "خانه‌ای در ابرها", Author = "بهرام اکبری", CategoryId = 4, Price = 240000m, PageCount = 136, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-11.svg", CreatedAt = new DateTime(2026, 10, 11, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 12, Title = "نقاشی با نور", Author = "سحر ملکی", CategoryId = 5, Price = 410000m, PageCount = 220, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-12.svg", CreatedAt = new DateTime(2026, 10, 12, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 13, Title = "قصه‌های سپید", Author = "آرزو کریمی", CategoryId = 4, Price = 190000m, PageCount = 96, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-13.svg", CreatedAt = new DateTime(2026, 10, 13, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 14, Title = "فیزیک در زندگی روزمره", Author = "علی کاظمی", CategoryId = 3, Price = 530000m, PageCount = 302, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-14.svg", CreatedAt = new DateTime(2026, 10, 14, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 15, Title = "در جست‌وجوی حقیقت", Author = "مریم صالحی", CategoryId = 2, Price = 345000m, PageCount = 250, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-15.svg", CreatedAt = new DateTime(2026, 10, 15, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 16, Title = "سیاره آبی", Author = "کاوه رحیمی", CategoryId = 3, Price = 260000m, PageCount = 200, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-16.svg", CreatedAt = new DateTime(2026, 10, 16, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 17, Title = "مبانی طراحی مدرن", Author = "نازنین رستگار", CategoryId = 5, Price = 680000m, PageCount = 380, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-17.svg", CreatedAt = new DateTime(2026, 10, 17, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 18, Title = "کوچه‌های قدیمی", Author = "امیر معینی", CategoryId = 1, Price = 385000m, PageCount = 290, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-18.svg", CreatedAt = new DateTime(2026, 10, 18, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 19, Title = "رنگ و فرم", Author = "نگار امینی", CategoryId = 5, Price = 425000m, PageCount = 248, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-19.svg", CreatedAt = new DateTime(2026, 10, 19, 12, 0, 0, DateTimeKind.Utc) });
        entity.HasData(new { Id = 20, Title = "روزی در جنگل", Author = "هانا موسوی", CategoryId = 4, Price = 165000m, PageCount = 112, Description = "کتابی برای مطالعه و تجربه‌ای تازه.", ImagePath = "/images/books/cover-20.svg", CreatedAt = new DateTime(2026, 10, 20, 12, 0, 0, DateTimeKind.Utc) });
    }
    public static void SeedUsers(EntityTypeBuilder<User> entity)
    {
        entity.HasData(new { Id = 1, Username = "admin", PasswordHash = "B8C9F26C53ED952588F8DF32BE2A1905B59C1811C95346C2B5279A37EC03D06F", PasswordSalt = "E6F5DE0CC109E35A53C01E5EF332DFE9", Role = UserRole.Admin, PenaltyAmount = 0m });
        entity.HasData(new { Id = 2, Username = "amir", PasswordHash = "ED9CE1E8135C52513A4E79DF417ECF8BF3822BD4C8503AE1F0446D1307E43B79", PasswordSalt = "383E81F7EE6BAD70227723400000F467", Role = UserRole.Member, PenaltyAmount = 0m });
        entity.HasData(new { Id = 3, Username = "sara", PasswordHash = "EC5E5370F38C02007527A78FBE48955F8099D49A0A03545E7260F564BF9B4FC0", PasswordSalt = "AA5F38B78D5701F244CB834B63A34EAE", Role = UserRole.Member, PenaltyAmount = 0m });
        entity.HasData(new { Id = 4, Username = "ali", PasswordHash = "35426E8BE52087A78BC79FF8A1912AEED39EF53C4E976184CD3FC46AD0033B1D", PasswordSalt = "8A66369AC071AE1513DF10F2BEE1EB1C", Role = UserRole.Member, PenaltyAmount = 0m });
    }
}
