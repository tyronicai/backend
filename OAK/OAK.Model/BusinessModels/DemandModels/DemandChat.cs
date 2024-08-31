using OAK.Model.BaseModels;
using OAK.Model.Core;

namespace OAK.Model.BusinessModels.DemandModels
{
    public class DemandChat : ModelBase
    {
        public int Id { get; set; }
        public int DemandId { get; set; }

        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }

        public virtual Demand Demand { get; set; }
        public virtual Account Account { get; set; }
    }
}