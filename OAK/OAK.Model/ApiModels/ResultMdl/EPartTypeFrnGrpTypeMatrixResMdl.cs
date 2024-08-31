using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.ResultModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EPartTypeFrnGrpTypeResMdl
    {
        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<EPartTypeFrnGrpType> EPartTypeFrnGrpTypeList { get; set; }
    }
}
