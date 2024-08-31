namespace OAK.Model.BusinessModels
{
    using OAK.Model.BaseModels;
    using OAK.Model.Core;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// indivudual, corporate
    /// </summary>
    public class CustomerType : LocalizationModelBase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "CustomerType.Name.Required")]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
