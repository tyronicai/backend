using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EstatePartTypeListResMdl
    {
        public EstatePartTypeListResMdl()
        {
            UeEstatePartTypeList = new List<UeEstatePartType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeEstatePartType> UeEstatePartTypeList { get; set; }
    }
}
