namespace reservation_backend.Services;

public class HashService
{
    public static (byte[], byte[]) HashInput(string input)
    {
        byte[] hash, salt;
        var hmac = new System.Security.Cryptography.HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        return (hash, salt);
    }
    
    public static bool IsHash(string input, byte[] hash, byte[] salt)
    {
        var hmac = new System.Security.Cryptography.HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        return computedHash.SequenceEqual(hash);
    }
}