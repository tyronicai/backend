using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EstateTypeListResMdl
    {
        public EstateTypeListResMdl()
        {
            UeEstateTypeList = new List<UeEstateType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeEstateType> UeEstateTypeList { get; set; }
    }
}
