namespace OAK.ServiceContracts
{
    using Microsoft.Extensions.DependencyInjection;
    using OAK.Model.ConfigurationModels;
    using OAK.Model.Core;

    public interface IAuthenticationProviderService
    {
        TokenSettings TokenSettings { get; }
        string GetToken(Account account);
        string GetRefresh(Account account);
        void ConfigureAuthentication(IServiceCollection serviceCollection);
    }

}
