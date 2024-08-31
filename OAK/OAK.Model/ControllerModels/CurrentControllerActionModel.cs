namespace OAK.Model.ControllerModels
{
    using System.Collections.Generic;

    public class CurrentControllerActionModel
    {
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public List<int> RoleIds { get; set; }

        public int AccountId { get; set; }

        public CurrentControllerActionModel()
        {
            RoleIds = new List<int>();
        }
    }
}
