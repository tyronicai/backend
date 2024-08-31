using OAK.Model.BaseModels;
using OAK.Model.Core;
using System.ComponentModel.DataAnnotations;

namespace OAK.Model.BusinessModels.AddressModels
{
    public class GenericAddress : ModelBase
    {
        public int Id { get; set; }


        public int? CountryId { get; set; }
        //public int? CityId { get; set; }

        /// <summary>
        /// nullable
        /// </summary>
        public string Town { get; set; }


        [Required(ErrorMessage = "GenericAddress.Street.Required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "GenericAddress.HouseNumber.Required")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "GenericAddress.PostCode.Required")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "GenericAddress.PlaceName.Required")]
        public string PlaceName { get; set; }

        [Required(ErrorMessage = "GenericAddress.GenericAddressTypeId.Required")]
        public int GenericAddressTypeId { get; set; }

        public virtual GenericAddressType GenericAddressType { get; set; }
        //public virtual City City { get; set; }
        public virtual Country Country { get; set; }
    }
}