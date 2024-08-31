
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OAK.WebApi.Models.RequestMdl
{
    public class EstatePartFurnitureAddReqMdl
    {
        public RequestBaseModel requestBaseMdl { get; set; }
        public EstatePartFurniture EstatePartFurniture { get; set; }
    }
}
