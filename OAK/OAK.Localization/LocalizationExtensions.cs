namespace OAK.Localization
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using DbLocalizationProvider;

    public class LocalizationExtensions
    {
        public static void ConfigureLocalization(IServiceCollection services)
        {
            services.AddLocalization();
            services.AddMvcCore().AddDataAnnotationsLocalization();
        }
    }
}
