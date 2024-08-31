using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class FurnitureTypeResMdl
    {
        public FurnitureTypeResMdl()
        {

        }
        public ResultBaseModel ResultBaseMdl { get; set; }
        public UeFurnitureType UeFurnitureType { get; set; }
    }
}
