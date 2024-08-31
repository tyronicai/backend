using OAK.Model.ResultModels;
using OAK.Model.ViewModels.DemandModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class DemandMetaDataResMdl
    {
        public DemandMetaDataResMdl()
        {
            ResultBaseMdl = new ResultBaseModel();
            UeDemandTypeList = new List<UeDemandType>();
            UeDemandStatusTypeList = new List<UeDemandStatusType>(); ;
        }

        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeDemandType> UeDemandTypeList { get; set; }
        public List<UeDemandStatusType> UeDemandStatusTypeList { get; set; }


    }
}
