using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DemandModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DemandStatusTypeListResMdl
    {
        public DemandStatusTypeListResMdl()
        {
            UeDemandStatusTypeList = new List<UeDemandStatusType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeDemandStatusType> UeDemandStatusTypeList { get; set; }
    }
}
