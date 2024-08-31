namespace OAK.ServiceContracts
{
    using OAK.Model.ControllerModels;
    using System.Collections.Generic;
    public interface IControllerDiscoveryService
    {
        IEnumerable<ControllerInfo> GetControllers();
    }

}
