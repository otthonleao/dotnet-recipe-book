using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptograhy;
using MyRecipeBook.Application.UserCases.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddAutoMapper(services);
        AddUserCases(services);
        AddPasswordSecurityService(services, configuration);
    }

    public static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddScoped(option => new AutoMapper.MapperConfiguration(Options =>
        {
            Options.AddProfile(new AutoMapping());
        }).CreateMapper());    
    }
    
    private static void AddUserCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserCase, RegisterUserCase>();
    }
    
    private static void AddPasswordSecurityService(this IServiceCollection services, IConfiguration configuration)
    {
        var additionalKey = configuration.GetValue<string>("Settings:Password:AdditionalKey");
        services.AddScoped(option => new PasswordSecurityService(additionalKey!));
    }
}