namespace OAK.Model.BusinessModels.DemandModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class ServiceType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
