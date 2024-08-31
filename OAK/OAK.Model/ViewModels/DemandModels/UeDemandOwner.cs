
namespace OAK.Model.ViewModels.DemandModels
{
    public class UeDemandOwner
    {
        public int Id { get; set; }

        public int DemandId { get; set; }

        public string Name { get; set; }
        public string EMail { get; set; }
        public string CountryPhoneCode { get; set; }
        public string PhoneNumber { get; set; }

        public string PreferredCulture { get; set; }
        public string AlternativeCulture { get; set; }

    }
}
