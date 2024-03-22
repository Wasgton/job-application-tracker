using System.Security.Cryptography;
using System.Text;

namespace JobApplicationTracker.Domain.Entities;

public class Password
{
    public static string ComputeHash(string password, string salt, string pepper, int iterations)
    {
        if (iterations <= 0) return password;
        using var sha256 = SHA256.Create();
        var passwordSaltPepper = $"{password}{salt}{pepper}";
        var byteValue = Encoding.UTF8.GetBytes(passwordSaltPepper);
        var byteHash = sha256.ComputeHash(byteValue);
        var hash = Convert.ToBase64String(byteHash);
        return ComputeHash(hash,salt,pepper,iterations-1);
    }

    public static string GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        return Convert.ToBase64String(byteSalt);
    }
}