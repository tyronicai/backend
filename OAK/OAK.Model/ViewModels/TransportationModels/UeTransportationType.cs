namespace OAK.Model.ViewModels.TransportationModels
{
    using OAK.Model.BaseModels;
    public class UeTransportationType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? PropertyJsonId { get; set; }

    }
}
