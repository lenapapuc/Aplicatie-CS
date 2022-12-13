using EasyEncryption;

namespace WebApplication3.Hashing_Of_Passwords;

public class SHA256
{
    public static string HashPassword(string input)
    {
        return SHA.ComputeSHA256Hash(input);
    }
}