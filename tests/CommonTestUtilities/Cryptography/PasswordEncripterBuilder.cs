using MyRecipeBook.Application.Services.Cryptograhy;

namespace CommonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static PasswordSecurityService Build() => new PasswordSecurityService("abc123");
}