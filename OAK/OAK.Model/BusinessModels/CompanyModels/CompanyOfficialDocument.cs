using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DocumentModels;

namespace OAK.Model.BusinessModels.CompanyModels
{
    public class CompanyOfficialDocument : ModelBase
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DocumentId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Document Document { get; set; }

    }
}
