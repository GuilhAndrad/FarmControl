using System.Security.Cryptography;
using System.Text;

namespace FarmControl.Application.Services.Cryptography;

public class PasswordEncryptor
{
    private readonly string _encryptionKey;
    public PasswordEncryptor(string encryptionKey)
    {
        _encryptionKey = encryptionKey;
    }
    public string Encrypt(string password)
    {
        var passwordAdditionalKey = $"{password}{_encryptionKey}";

        var bytes = Encoding.UTF8.GetBytes(password);
        var sha512 = SHA512.Create();
        byte[] hasBytes = sha512.ComputeHash(bytes);
        return StringBytes(hasBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }
        return sb.ToString();
    }
}