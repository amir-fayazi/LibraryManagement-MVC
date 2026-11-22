using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.Json;
namespace Library.Infrastructure.Data;
public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
{
    public LibraryDbContext CreateDbContext(string[] args)
    {
        var connection = Environment.GetEnvironmentVariable("LIBRARY_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connection))
        {
            var dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (dir is not null)
            {
                var candidates = new[]
                {
                    Path.Combine(dir.FullName, "appsettings.json"),
                    Path.Combine(dir.FullName, "04.Web", "Library.Web", "appsettings.json")
                };
                var path = candidates.FirstOrDefault(File.Exists);
                if (path is not null)
                {
                    using var doc = JsonDocument.Parse(File.ReadAllText(path));
                    if (doc.RootElement.TryGetProperty("ConnectionStrings", out var strings) &&
                        strings.TryGetProperty("DefaultConnection", out var value)) connection = value.GetString();
                    break;
                }
                dir = dir.Parent;
            }
        }
        connection ??= @"Server=(localdb)\MSSQLLocalDB;Database=LibraryWebDemo;Trusted_Connection=True;TrustServerCertificate=True";
        var opts = new DbContextOptionsBuilder<LibraryDbContext>().UseSqlServer(connection).Options;
        return new LibraryDbContext(opts);
    }
}
