using OAK.Model.BaseModels;
using System.Collections.Generic;

namespace OAK.Model.BusinessModels.CompanyModels
{
    public class CompanyStatusType : LocalizationModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
