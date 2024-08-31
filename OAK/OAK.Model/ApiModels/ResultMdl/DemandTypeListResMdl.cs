using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DemandModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DemandTypeListResMdl
    {
        public DemandTypeListResMdl()
        {
            UeDemandTypeList = new List<UeDemandType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeDemandType> UeDemandTypeList { get; set; }
    }
}
