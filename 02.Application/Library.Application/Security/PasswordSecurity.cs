using System.Security.Cryptography;
using System.Text;
namespace Library.Application.Security;
public static class PasswordSecurity
{
    public static (string Hash, string Salt) Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var bytes = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, 120000, HashAlgorithmName.SHA256, 32);
        return (Convert.ToHexString(bytes), Convert.ToHexString(salt));
    }
    public static bool Verify(string password, string expectedHash, string salt)
    {
        try
        {
            var bytes = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), Convert.FromHexString(salt), 120000, HashAlgorithmName.SHA256, 32);
            return CryptographicOperations.FixedTimeEquals(bytes, Convert.FromHexString(expectedHash));
        }
        catch (FormatException) { return false; }
    }
}
