using OAK.Model.BaseModels;

namespace OAK.Model.BusinessModels.DemandModels
{
    public class DemandStatusType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
