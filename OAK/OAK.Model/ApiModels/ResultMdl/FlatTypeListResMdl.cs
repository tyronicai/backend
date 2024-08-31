using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class FlatTypeListResMdl
    {
        public FlatTypeListResMdl()
        {
            UeFlatTypeList = new List<UeFlatType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeFlatType> UeFlatTypeList { get; set; }
    }
}
