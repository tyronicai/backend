using OAK.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAK.Model.ApiModels.EstateModels
{
    public class EstatePartMdl 
    {
        public int Id { get; set; }
        public int EstatePartTypeId { get; set; }
        public string PropertyValues { get; set; }
        public List<FurnitureMdl> furnitureMdls { get; set; }


    }
}
