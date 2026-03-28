using System.Globalization;
using Babko_lab4.dao;
using Microsoft.AspNetCore.Localization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        var app = builder.Build();

        var defaultCulture = new CultureInfo("en-US");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        };
        app.UseRequestLocalization(localizationOptions);

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        var applicationLifeTime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        applicationLifeTime.ApplicationStopping.Register(OnShutdown);
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Goods}/{action=GetAll}"
        );

        app.Run();
    }

    public static void OnShutdown()
    {
        NHibernateDAOFactory.getInstance().Destroy();
    }
}