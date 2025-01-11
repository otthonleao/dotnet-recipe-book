using System.Globalization;

namespace MyRecipeBook.API.Middleware;

public class CultureMiddleware
{
    
    private readonly RequestDelegate _next;
    
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);
        
        var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();
        
        var cultureInfo = new CultureInfo("en");
        
        if (string.IsNullOrWhiteSpace(culture) == false && supportedLanguages.Any(x => x.Name == culture))
        {
            cultureInfo = new CultureInfo(culture);
        }
        
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        
        await _next(context);
    }
}