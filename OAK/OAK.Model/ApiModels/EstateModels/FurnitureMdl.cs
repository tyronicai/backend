using OAK.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAK.Model.ApiModels.EstateModels
{
    public class FurnitureMdl 
    {
        public int Id { get; set; }
        public int FurnitureTypeId { get; set; }
        public string PropertyValues { get; set; }
    }
}
