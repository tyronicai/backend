
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OAK.Model.BusinessModels.CompanyModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    public class Company : ModelBase

    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Company.AccountId.Required")]
        public int AccountId { get; set; }

        [Required(ErrorMessage = "Company.Name.Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company.Email.Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company.PhoneNumber.Required")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Company.CompanyStatusTypeId.Required")]
        public int CompanyStatusTypeId { get; set; }
        public virtual CompanyStatusType CompanyStatusType { get; set; }

        [Required(ErrorMessage = "Company.GenericAddressId.Required")]
        public int GenericAddressId { get; set; }

        [Required(ErrorMessage = "Company.TaxNumber.Required")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "Company.Guid.Required")]
        public string Guid { get; set; }

        public virtual ICollection<CompanyPostCodeData> CompanyPostCodeDatas { get; set; }

        //public virtual Account Account { get; set; }
        //public virtual GenericAddress GenericAddress { get; set; }


    }
}
