namespace OAK.Model.ViewModels.TransportationModels
{
    using OAK.Model.BaseModels;
    public class UeTransportationStatusType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int? PropertyJsonId { get; set; }
    }
}
