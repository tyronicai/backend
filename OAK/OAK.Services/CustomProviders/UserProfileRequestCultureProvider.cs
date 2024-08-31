namespace OAK.Services.CustomProviders
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;
    using System;
    using System.Threading.Tasks;

    public class UserProfileRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string culture = null;
            string uiCulture = null;
            try
            {
                culture = httpContext.Request.Path.Value.Split('/')[2]?.ToString();
                if (culture == null)
                    return Task.FromResult((ProviderCultureResult)null);

                var providerResultCulture = new ProviderCultureResult(culture, uiCulture);
                return Task.FromResult(providerResultCulture);
            }
            catch (Exception ex)
            {
                return Task.FromResult((ProviderCultureResult)null);
            }
        }
    }
}
