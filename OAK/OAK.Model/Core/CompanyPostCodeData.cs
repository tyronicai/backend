using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CompanyModels;
using System.ComponentModel.DataAnnotations;

namespace OAK.Model.Core
{
    public class CompanyPostCodeData : ModelBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "CompanyPostCodeData.CountryId.Required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "CompanyPostCodeData.CompanyId.Required")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "CompanyPostCodeData.PostCode.Required")]
        public string PostCode { get; set; }

        public virtual Country Country { get; set; }
        public virtual Company Company { get; set; }


    }
}