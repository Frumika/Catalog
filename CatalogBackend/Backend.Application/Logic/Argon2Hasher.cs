using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Backend.Application.Logic;

public static class Argon2Hasher
{
    public static string HashPassword(string @string)
    {
        byte[] salt = new byte[16];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(@string))
        {
            Salt = salt,
            DegreeOfParallelism = 4,
            MemorySize = 65536,
            Iterations = 4
        };

        byte[] hash = argon2.GetBytes(32);

        string result = Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);

        return result;
    }
    
    public static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2) return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] storedKey = Convert.FromBase64String(parts[1]);

        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 4,
            MemorySize = 65536,
            Iterations = 4
        };

        byte[] key = argon2.GetBytes(32);
        return CryptographicOperations.FixedTimeEquals(key, storedKey);
    }
}