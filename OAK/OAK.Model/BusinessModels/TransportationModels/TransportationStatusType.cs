namespace OAK.Model.BusinessModels.TransportationModels
{
    using OAK.Model.BaseModels;
    public class TransportationStatusType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

    }
}
