namespace OAK.Services
{
    using Microsoft.Extensions.DependencyInjection;
    using OAK.Model.ControllerModels;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;
    using OAK.Services.PermissionHandlers.Requirements;

    public class AccountPermissionService : IAccountPermissionService
    {
        public bool CanCurrentAccountAccess(CurrentControllerActionModel currentControllerActionModel)
        {
            return true;
        }

        public bool IsEmailActivationCompleted()
        {
            // TODO
            return true;
        }

        public void ConfigureAuthentication(IServiceCollection serviceCollection)
        {
            serviceCollection.AddAuthorization(opts =>
            {
                opts.AddPolicy(
                    name: AppStaticValues.RoleBasedPermissionName,
                    configurePolicy: policy =>
                    {
                        policy.RequireAuthenticatedUser();
                        policy.Requirements.Add(new RoleBasedPermissionRequirement());
                        policy.Requirements.Add(new EmailValidationRequirement());
                    });
            });

            // serviceCollection.AddAuthorization(opts =>
            // {
            //     opts.AddPolicy(
            //         name: AppStaticValues.AuthenticatePermissionName,
            //         configurePolicy: policy =>
            //         {
            //             policy.RequireAuthenticatedUser();
            //         });
            // });
        }
    }
}
