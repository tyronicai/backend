namespace OAK.Services
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using OAK.Model.ControllerModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public class ControllerDiscoveryService : IControllerDiscoveryService
    {
        private IActionDescriptorCollectionProvider ActionDescriptorCollectionProvider;

        private List<ControllerInfo> _mvcControllers;

        public ControllerDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            ActionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        public IEnumerable<ControllerInfo> GetControllers()
        {
            if (_mvcControllers != null)
                return _mvcControllers;

            _mvcControllers = new List<ControllerInfo>();

            var items = ActionDescriptorCollectionProvider
                .ActionDescriptors.Items
                .Where(descriptor => descriptor.GetType() == typeof(ControllerActionDescriptor))
                .Select(descriptor => (ControllerActionDescriptor)descriptor)
                .GroupBy(descriptor => descriptor.ControllerTypeInfo.FullName)
                .ToList();

            foreach (var actionDescriptors in items)
            {
                if (!actionDescriptors.Any())
                    continue;

                var actionDescriptor = actionDescriptors.First();
                var controllerTypeInfo = actionDescriptor.ControllerTypeInfo;
                var currentController = new ControllerInfo
                {
                    AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    DisplayName = controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    Name = actionDescriptor.ControllerName,
                };

                var actions = new List<ActionInfo>();
                foreach (var descriptor in actionDescriptors.GroupBy
                                            (a => a.ActionName).Select(g => g.First()))
                {
                    var methodInfo = descriptor.MethodInfo;
                    actions.Add(new ActionInfo
                    {
                        ControllerId = currentController.Id,
                        Name = descriptor.ActionName,
                        DisplayName = methodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    });
                }

                currentController.Actions = actions;
                _mvcControllers.Add(currentController);
            }

            return _mvcControllers;
        }
    }
}
