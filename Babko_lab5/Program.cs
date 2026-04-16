using System.Globalization;
using Babko_lab5.Dao;
using Microsoft.AspNetCore.Localization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers().AddXmlSerializerFormatters();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        var defaultCulture = new CultureInfo("en-US");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        };
        app.UseRequestLocalization(localizationOptions);

        app.UseAuthorization();
        app.MapControllers();
        var applicationLifeTime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        applicationLifeTime.ApplicationStopping.Register(OnShutdown);

        app.Run();
    }

    public static void OnShutdown()
    {
        NHibernateDAOFactory.getInstance().Destroy();
    }
}