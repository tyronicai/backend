using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;
using OAK.CompanyServices;
using OAK.TansportationServices;
using OAK.Validation.CompanyValidation;
using OAK.Validation.CompanyValidation.Interfaces;
using OAK.Validation.TokenValidation;
using OAK.Validation.TokenValidation.Interfaces;

namespace OAK.WebApi
{

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.HttpOverrides;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Options;

    using Npgsql.EntityFrameworkCore.PostgreSQL;
    using OAK.Data;
    using OAK.DataBase;
    using OAK.EstateServices;
    using OAK.InitializationServices;
    using OAK.Localizer.DbStringLocalizer;
    using OAK.Logging;
    using OAK.Logging.Interfaces;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;
    using OAK.Services;
    using OAK.Services.CustomProviders;
    using OAK.Services.PermissionHandlers;
    using OAK.Validation.AccountValidation;
    using OAK.Validation.AccountValidation.Interfaces;
    using System.Globalization;
    using System.Reflection;
    using AutoMapper;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public IConfiguration Configuration { get; }
        readonly string MyAllowAllOrigins = "_myAllowAllOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string connectionString = Configuration.GetConnectionString(AppStaticValues.DefaultConnectionName);


            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DbContextDefault>();
            //dbContextOptionsBuilder.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60)).EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            dbContextOptionsBuilder.UseNpgsql(connectionString,
                    providerOptions => providerOptions.CommandTimeout(60).MigrationsAssembly("OAK.WebApi"))
                .EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            //services.AddDbContext<DbContextDefault>(options => options.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60)));
            //services.AddDbContext<DbContextDefault>(options => options.UseNpgsql(connectionString, providerOptions => providerOptions.CommandTimeout(60)));
            services.AddDbContext<DbContextDefault>(options =>
                options.UseNpgsql(connectionString,
                        providerOptions => providerOptions.CommandTimeout(60).MigrationsAssembly("OAK.WebApi"))
                    .EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            using (DbContextDefault dbContextDefault = new DbContextDefault(dbContextOptionsBuilder.Options))
            {
                // dbContextDefault.Database.EnsureCreated();
                dbContextDefault.Database.Migrate();
            }

            var localizationContextOptionsBuilder = new DbContextOptionsBuilder<LocalizationModelContext>();
            //localizationContextOptionsBuilder.UseSqlServer(connectionString, providerOptions => providerOptions.CommandTimeout(60)).EnableSensitiveDataLogging(true);
            localizationContextOptionsBuilder
                .UseNpgsql(connectionString, providerOptions => providerOptions.CommandTimeout(60))
                .EnableSensitiveDataLogging(true).UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

            services.AddDbContext<LocalizationModelContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly("OAK.Model")
                ),
                ServiceLifetime.Singleton,
                ServiceLifetime.Singleton
            );



            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowAllOrigins,
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://meinumzug24.eu",
                                "https://meinumzug24.eu",
                                "http://api.meinumzug24.eu",
                                "https://api.meinumzug24.eu",
                                "http://admin.meinumzug24.eu",
                                "https://admin.meinumzug24.eu",
                                "http://mymove24.com",
                                "https://mymove24.com",
                                "http://www.mymove24.com",
                                "https://www.mymove24.com",
                                "http://api.mymove24.com",
                                "https://api.mymove24.com",
                                "http://admin.mymove24.com",
                                "https://admin.mymove24.com",
                                "http://localhost:5000",
                                "http://localhost",
                                "http://localhost:4200",
                                "http://localhost:8100",
                                "http://10.0.2.2:5000")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();

                    });
            });



            var returnOnlyKeyIfNotFound = false;
            var createNewRecordWhenLocalisedStringDoesNotExist = true;

            services.AddSqlLocalization(options => options.UseSettings(
                returnOnlyKeyIfNotFound,
                createNewRecordWhenLocalisedStringDoesNotExist));

            services.AddAutoMapper(typeof(OAK.Model.BusinessModels.CompanyModels.Company).Assembly);

            services.AddMvc(options => options.EnableEndpointRouting = true)
                //.SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddViewLocalization()
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            var localizationSettingsSection = Configuration.GetSection(AppStaticValues.LocalizationSettingsSectionName);
            var localizationSettings = localizationSettingsSection.Get<LocalizationSettings>();

            //optionsLoc.RequestCultureProviders.Clear();
            //optionsLoc.RequestCultureProviders.Add(new RouteDataRequestCultureProvider());

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: localizationSettings.DefaultRequestCulture, uiCulture: localizationSettings.DefaultRequestUICulture);
                options.SupportedCultures = localizationSettings.SupportedCultureList;
                options.SupportedUICultures = localizationSettings.SupportedCultureList;
                options.RequestCultureProviders.Clear(); // Clears all the default culture providers from the list
                options.RequestCultureProviders.Add(new UserProfileRequestCultureProvider() { Options = options }); // Add your custom culture provider back to the list

            });

            var accSettingsSection = Configuration.GetSection(AppStaticValues.AccountSettingsSectionName);
            services.Configure<AccountSettings>(accSettingsSection);

            var smtpSettingsSection = Configuration.GetSection(AppStaticValues.SmtpSettingsSectionName);
            services.Configure<SmtpSettings>(smtpSettingsSection);

            var tokenSettingsSection = Configuration.GetSection(AppStaticValues.TokenSettingsSectionName);
            services.Configure<TokenSettings>(tokenSettingsSection);

            var passwordSettings = Configuration.GetSection(AppStaticValues.HashPasswordSettingsSectionName);
            services.Configure<HashPasswordSettings>(passwordSettings);

            var documentSettings = Configuration.GetSection(AppStaticValues.DocumentSettingsSectionName);
            services.Configure<DocumentSettings>(documentSettings);

            var fcmSettings = Configuration.GetSection(AppStaticValues.NotificationSettingsSectionName);
            services.Configure<NotificationSettings>(fcmSettings);

            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();
            IAuthenticationProviderService authenticationProviderService = new AuthenticationProviderService(tokenSettings);
            authenticationProviderService.ConfigureAuthentication(services);

            IAccountPermissionService accountPermissionService = new AccountPermissionService();
            accountPermissionService.ConfigureAuthentication(services);

            services.AddSingleton<ILoggerManager, LoggerManagerDefault>();
            services.AddSingleton<IControllerDiscoveryService, ControllerDiscoveryService>();
            services.AddSingleton<IAuthenticationProviderService, AuthenticationProviderService>();
            services.AddSingleton<IAccountPermissionService, AccountPermissionService>();
            services.AddSingleton<IAuthorizationHandler, GeneralPermissionHandler>();


            services.AddScoped<ILocalizationService, LocalizationService>();

            services.AddScoped<ILanguageService, LanguageService>();

            services.AddScoped<ICountryService, CountryService>();

            services.AddScoped<IPasswordHasherService, PasswordHasherService>();


            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IUnitOfWork, UnitOfWork<DbContextDefault>>();


            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<INotificationService, NotificationService>();

            //
            //
            // Validators
            //

            #region Validators

            services.AddScoped<ITokenValidator, TokenValidation>();
            services.AddScoped<IAccountValidator, AccountValidation>();
            services.AddScoped<ICompanyValidator, CompanyValidation>();

            #endregion Validators

            //
            // Business Services
            //

            #region GenericAddressService
            services.AddScoped<IGenericAddressService, GenericAddressService>();
            #endregion GenericAddressService

            #region CommentService
            services.AddScoped<ICommentService, CommentService>();
            #endregion CommentService

            #region DemandService
            services.AddScoped<IDemandService, DemandService>();
            #endregion DemandService

            #region CompanyService
            services.AddScoped<ICompanyService, CompanyService>();
            #endregion CompanyService

            #region DocumentService
            services.AddScoped<IDocumentService, DocumentService>();
            #endregion DocumentService

            #region EstateService
            services.AddScoped<IEstateService, EstateService>();
            #endregion EstateService

            #region PropertyJsonService
            services.AddScoped<IPropertyJsonService, PropertyJsonService>();
            #endregion PropertyJsonService

            #region TransportationService
            services.AddScoped<ITransportationService, TransportationService>();
            #endregion TransportationService

            #region ParameterService
            services.AddSingleton<IParameterService, ParameterService>();
            #endregion ParameterService

            #region InitializationService
            services.AddTransient<IInitializationService, InitializationService>();

            #endregion InitializationService

            services.AddControllers();


        }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net("log4net.config", true);

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseStaticFiles();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(MyAllowAllOrigins);
            }
            else
            {
                app.UseCors(MyAllowAllOrigins);
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireCors(MyAllowAllOrigins);
            });
        }
    }

}
