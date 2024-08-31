using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.ResultModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EstateTypeEPartTypeResMdl
    {
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<EstateTypeEPartType> EstateTypeEPartTypeList { get; set; }
    }
}
