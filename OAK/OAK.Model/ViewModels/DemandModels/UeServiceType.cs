namespace OAK.Model.ViewModels.DemandModels
{
    using OAK.Model.BaseModels;
    public class UeServiceType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? PropertyJsonId { get; set; }
    }
}
