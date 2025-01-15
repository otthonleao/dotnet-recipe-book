using System.Security.Cryptography;
using System.Text;

namespace MyRecipeBook.Application.Services.Cryptograhy;

public class PasswordSecurityService
{
    private readonly string _additionalKey;

    public PasswordSecurityService(string additionalKey)
    {
        _additionalKey = additionalKey;
    }

    public string GenerateHash(string password)
    {
        var newPassword = $"{password}{_additionalKey}";
        
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = SHA512.HashData(bytes);

        return StringBytes(hash);
    }
    
    private static string StringBytes(byte[] bytes)
    {
        var builder = new StringBuilder();
        
        foreach (byte item in bytes)
        {
            var hex = item.ToString("X2");
            builder.Append(hex);
        }
        
        return builder.ToString();
    }
}