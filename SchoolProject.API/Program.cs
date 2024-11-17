using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service;
using System.Globalization;
namespace SchoolProject.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(
            options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("constr"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
        );

        #region Dependency Injection

        builder.Services.AddInfrastructureDependancies()
            .AddServiceDependancies()
            .AddCoreDependancies();
        #endregion


        #region Localization
        builder.Services.AddControllersWithViews();
        builder.Services.AddLocalization(opt => opt.ResourcesPath = "");
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("ar-EG"),
            };
            options.DefaultRequestCulture = new RequestCulture("ar-EG");
            //options.DefaultRequestCulture = new RequestCulture("en-US");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });
        #endregion

        #region AllowCors
        var CORS = "_cors";
        builder.Services.AddCors(options =>
        {
            //options.AddPolicy(name: "CORS", policy => policy.WithOrigins("https://localhost:5173/"));
            options.AddPolicy(name: CORS, policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });
        #endregion

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ErrorHandlerMiddleware>();

        #region Localization Middleware
        var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(options!.Value);
        #endregion

        app.UseHttpsRedirection();

        app.UseCors(CORS);

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
