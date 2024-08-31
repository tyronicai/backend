namespace OAK.ServiceContracts
{
    using Microsoft.Extensions.DependencyInjection;
    using OAK.Model.ControllerModels;

    public interface IAccountPermissionService
    {
        bool CanCurrentAccountAccess(CurrentControllerActionModel currentControllerActionModel);

        bool IsEmailActivationCompleted();
        void ConfigureAuthentication(IServiceCollection serviceCollection);
    }

}
