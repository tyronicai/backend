namespace OAK.Model.BusinessModels.AddressModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class GenericAddressType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "GenericAddressType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int? PropertyJsonId { get; set; }

        public virtual PropertyJson PropertyJson { get; set; }
        public virtual ICollection<GenericAddress> GenericAddresses { get; set; }
    }
}
