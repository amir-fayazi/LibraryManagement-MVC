using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    PenaltyAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true),
                    FineAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsReturned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookLoans_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookLoans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 1, "معاصر و کلاسیک", "/images/categories/fiction.svg", "رمان و داستان" },
                    { 2, "غرب و شرق", "/images/categories/philosophy.svg", "فلسفه و منطق" },
                    { 3, "فناوری و تاریخ", "/images/categories/science.svg", "علمی و تاریخی" },
                    { 4, "داستان‌های مصور", "/images/categories/children.svg", "کودک و نوجوان" },
                    { 5, "طراحی و نقاشی", "/images/categories/art.svg", "هنر و معماری" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "PasswordHash", "PasswordSalt", "PenaltyAmount", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "B8C9F26C53ED952588F8DF32BE2A1905B59C1811C95346C2B5279A37EC03D06F", "E6F5DE0CC109E35A53C01E5EF332DFE9", 0m, 1, "admin" },
                    { 2, "ED9CE1E8135C52513A4E79DF417ECF8BF3822BD4C8503AE1F0446D1307E43B79", "383E81F7EE6BAD70227723400000F467", 0m, 2, "amir" },
                    { 3, "EC5E5370F38C02007527A78FBE48955F8099D49A0A03545E7260F564BF9B4FC0", "AA5F38B78D5701F244CB834B63A34EAE", 0m, 2, "sara" },
                    { 4, "35426E8BE52087A78BC79FF8A1912AEED39EF53C4E976184CD3FC46AD0033B1D", "8A66369AC071AE1513DF10F2BEE1EB1C", 0m, 2, "ali" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CategoryId", "CreatedAt", "Description", "ImagePath", "PageCount", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "آنتونیو مارکز", 1, new DateTime(2026, 11, 21, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/last-breath.jpg", 400, 490000m, "آخرین نفس" },
                    { 2, "لورا هریس", 3, new DateTime(2026, 11, 20, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/journey-to-mars.jpg", 280, 620000m, "سفر به مریخ" },
                    { 3, "دکتر جان اسمیت", 2, new DateTime(2026, 11, 19, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/modern-leadership.jpg", 310, 750000m, "اصول رهبری مدرن" },
                    { 4, "ماروسیو کانت", 2, new DateTime(2026, 11, 18, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/ancient-wisdom.jpg", 550, 380000m, "حکمت‌های کهن" },
                    { 5, "دایان وایلد", 3, new DateTime(2026, 11, 17, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/twentieth-century-changes.jpg", 440, 550000m, "تغییرات قرن بیستم" },
                    { 6, "نیلوفر شریفی", 1, new DateTime(2026, 10, 6, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-06.svg", 212, 320000m, "پشت پنجره باران" },
                    { 7, "سارا رضایی", 2, new DateTime(2026, 10, 7, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-07.svg", 180, 210000m, "هنر ساده زیستن" },
                    { 8, "رضا محسنی", 3, new DateTime(2026, 10, 8, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-08.svg", 364, 460000m, "تاریخ کوتاه جهان" },
                    { 9, "پریسا احمدی", 1, new DateTime(2026, 10, 9, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-09.svg", 260, 290000m, "شهر کاغذی" },
                    { 10, "مینا رضایی", 4, new DateTime(2026, 10, 10, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-10.svg", 120, 175000m, "ستاره‌های کوچک" },
                    { 11, "بهرام اکبری", 4, new DateTime(2026, 10, 11, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-11.svg", 136, 240000m, "خانه‌ای در ابرها" },
                    { 12, "سحر ملکی", 5, new DateTime(2026, 10, 12, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-12.svg", 220, 410000m, "نقاشی با نور" },
                    { 13, "آرزو کریمی", 4, new DateTime(2026, 10, 13, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-13.svg", 96, 190000m, "قصه‌های سپید" },
                    { 14, "علی کاظمی", 3, new DateTime(2026, 10, 14, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-14.svg", 302, 530000m, "فیزیک در زندگی روزمره" },
                    { 15, "مریم صالحی", 2, new DateTime(2026, 10, 15, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-15.svg", 250, 345000m, "در جست‌وجوی حقیقت" },
                    { 16, "کاوه رحیمی", 3, new DateTime(2026, 10, 16, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-16.svg", 200, 260000m, "سیاره آبی" },
                    { 17, "نازنین رستگار", 5, new DateTime(2026, 10, 17, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-17.svg", 380, 680000m, "مبانی طراحی مدرن" },
                    { 18, "امیر معینی", 1, new DateTime(2026, 10, 18, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-18.svg", 290, 385000m, "کوچه‌های قدیمی" },
                    { 19, "نگار امینی", 5, new DateTime(2026, 10, 19, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-19.svg", 248, 425000m, "رنگ و فرم" },
                    { 20, "هانا موسوی", 4, new DateTime(2026, 10, 20, 12, 0, 0, 0, DateTimeKind.Utc), "کتابی برای مطالعه و تجربه‌ای تازه.", "/images/books/cover-20.svg", 112, 165000m, "روزی در جنگل" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans",
                column: "BookId",
                unique: true,
                filter: "[IsReturned] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoans_UserId_IsReturned",
                table: "BookLoans",
                columns: new[] { "UserId", "IsReturned" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CreatedAt",
                table: "Books",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId_BookId",
                table: "Reviews",
                columns: new[] { "UserId", "BookId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_BookId",
                table: "Wishlists",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId_BookId",
                table: "Wishlists",
                columns: new[] { "UserId", "BookId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLoans");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
