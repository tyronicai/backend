using Microsoft.AspNetCore.Routing;

namespace OAK.Services.PermissionHandlers
{
    using Microsoft.AspNetCore.Authorization;
    using OAK.Model.ControllerModels;
    using OAK.ServiceContracts;
    using OAK.Services.PermissionHandlers.Requirements;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class GeneralPermissionHandler : IAuthorizationHandler
    {
        private IAccountPermissionService AccountPermissionService;
        public GeneralPermissionHandler(IAccountPermissionService accountPermissionService)
        {
            AccountPermissionService = accountPermissionService;
        }

        #region HandleAsync

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var routeEndpoint = context.Resource as RouteEndpoint;
            if (routeEndpoint == null)
                return Task.CompletedTask;

            var user = context.User;
            var routePattern = routeEndpoint.RoutePattern;
            CurrentControllerActionModel currentControllerActionModel = new CurrentControllerActionModel();

            foreach (var item in user.Claims)
            {
                if (item.Type == ClaimTypes.Role)
                    currentControllerActionModel.RoleIds.Add(int.Parse(item.Value));
                else if (item.Type == ClaimTypes.NameIdentifier)
                    currentControllerActionModel.AccountId = int.Parse(item.Value);
            }

            var controllerPart = routePattern.RequiredValues["controller"] as string;
            var actionPart = routePattern.RequiredValues["action"] as string;

            //var actionDescriptor = mvcContext.ActionDescriptor;

            //currentControllerActionModel.AreaName = actionDescriptor.RouteValues["area"];
            currentControllerActionModel.ControllerName = controllerPart;//actionDescriptor.RouteValues["controller"];
            currentControllerActionModel.ActionName = actionPart;//actionDescriptor.RouteValues["action"];

            var pendings = context.PendingRequirements.ToList();

            foreach (var requirement in pendings)
            {
                if (requirement is RoleBasedPermissionRequirement)
                {
                    if (AccountPermissionService.CanCurrentAccountAccess(currentControllerActionModel))
                        context.Succeed(requirement);
                    else
                        context.Fail();
                }
                else if (requirement is EmailValidationRequirement)
                {
                    if (AccountPermissionService.IsEmailActivationCompleted())
                        context.Succeed(requirement);
                    else
                        context.Fail();
                }
            }

            return Task.CompletedTask;
        }

        #endregion

        #region NewAsyncHandle

        #endregion
    }
}
