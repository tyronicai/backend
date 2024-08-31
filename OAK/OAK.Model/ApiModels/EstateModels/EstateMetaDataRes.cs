using OAK.Model.BusinessModels.EstateModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAK.Model.ApiModels.EstateModels
{
    public class EstateMetaDataRes
    {
        public List<EstateType> estateTypes { get; set; }
        public List<EstatePartType> estatePartTypes { get; set; }
        public List<FurnitureType> furnitureTypes { get; set; }
        public List <EstateTypeEPartTypeMatrix> estateTypeEPartTypeMatrices { get; set; }
        public List<EPartTypeFrnTypeMatrix> ePartTypeFrnTypeMatrices { get; set; }
    }
}
