using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class FurnitureTypeListResMdl
    {
        public FurnitureTypeListResMdl()
        {
            UeFurnitureTypeList = new List<UeFurnitureType>();
            ResultBaseMdl = new ResultBaseModel();
        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeFurnitureType> UeFurnitureTypeList { get; set; }
    }
}
