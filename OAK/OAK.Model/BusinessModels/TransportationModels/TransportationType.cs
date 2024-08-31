namespace OAK.Model.BusinessModels.TransportationModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    public class TransportationType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
    }
}
