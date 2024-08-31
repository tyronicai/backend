using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EstateTypeResMdl
    {
        public EstateTypeResMdl()
        {

        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeEstateType UeEstateType { get; set; }
    }
}
