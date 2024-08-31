using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OAK.DataBase;
using OAK.ServiceContracts;
using System;
using System.Threading.Tasks;

namespace OAK.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DbContextDefault>();
                    await context.Database.MigrateAsync();

                    var scopedInitializationService = services.GetRequiredService<IInitializationService>();
                    if (!scopedInitializationService.IsDatabasePopulated())
                    {
                        scopedInitializationService.AddAdminAccount();
                        scopedInitializationService.AddDefaultLanguages();
                        scopedInitializationService.AddEstateTypes();
                        scopedInitializationService.AddFlatTypes();
                        scopedInitializationService.AddEstatePartTypes();
                        scopedInitializationService.AddFurnitureCalculationTypes();
                        scopedInitializationService.AddFurnitureGroupTypes();
                        scopedInitializationService.AddFurnitureTypes();
                        scopedInitializationService.AddEPartTypeFrnGrpType();
                        scopedInitializationService.EstateTypeEPartType();
                        scopedInitializationService.AddDemandTypes();
                        scopedInitializationService.AddDemandStatusTypes();
                        scopedInitializationService.AddAdressTypes();
                        scopedInitializationService.AddCountries();
                        scopedInitializationService.AddCompanyStatusTypes();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or initializing the database.");
                }
            }

            // Configure the HTTP request pipeline.
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            startup.Configure(app, app.Environment, loggerFactory);


            await app.RunAsync();
        }

        // EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
    }
}