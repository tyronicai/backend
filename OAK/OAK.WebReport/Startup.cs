using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OAK.DataBase;
using OAK.Model.ConfigurationModels;
using OAK.Model.Core;

using OAK.Model.StaticModels;
using OAK.ServiceContracts;
using OAK.WebReport.Services;
using IReportService = OAK.WebReport.Services.ServiceContracts.IReportService;

namespace OAK.WebReport
{
    using OAK.Data;
    using OAK.Data.Paging;
    using OAK.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            string connectionString = Configuration.GetConnectionString(AppStaticValues.DefaultConnectionName);

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DbContextDefault>();
            //dbContextOptionsBuilder.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60)).EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            dbContextOptionsBuilder.UseNpgsql(connectionString,
                    providerOptions => providerOptions.CommandTimeout(60).MigrationsAssembly("OAK.WebReport"))
                .EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            services.AddDbContext<DbContextDefault>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("OAKConnection")));

            var smtpSettingsSection = Configuration.GetSection(AppStaticValues.SmtpSettingsSectionName);
            services.Configure<SmtpSettings>(smtpSettingsSection);

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IUnitOfWork, UnitOfWork<DbContextDefault>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The defaultapi/values HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}