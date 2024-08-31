using System.ComponentModel.DataAnnotations;

namespace OAK.Model.ViewModels
{
    public class UeGenericAddress
    {
        public int Id { get; set; }


        public int? CountryId { get; set; }
        // public int? CityId { get; set; }

        /// <summary>
        /// nullable
        /// </summary>
        public string Town { get; set; }

        [Required(ErrorMessage = "GenericAddress.PostCode.Required")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "GenericAddress.Street.Required")]
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PlaceName { get; set; }

        public int GenericAddressTypeId { get; set; }

    }
}