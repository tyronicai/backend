
namespace OAK.Model.ViewModels.DemandModels
{
    public class UeDemand
    {
        public int Id { get; set; }

        public int DemandTypeId { get; set; }

        public int? AccountId { get; set; }
        public string PropertyValues { get; set; }
        public int DemandStatusTypeId { get; set; }
        public int DemandOwnerId { get; set; }
        public UeDemandOwner DemandOwner { get; set; }

    }
}
