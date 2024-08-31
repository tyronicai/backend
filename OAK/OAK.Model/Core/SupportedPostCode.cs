namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;

    public class SupportedPostCode : ModelBase
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string PostCode { get; set; }

        public virtual Country Country { get; set; }
    }
}
