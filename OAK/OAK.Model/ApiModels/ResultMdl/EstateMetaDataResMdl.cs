using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.ResultModels;
using OAK.Model.ViewModels.EstateModels;
using System.Collections.Generic;

namespace OAK.Model.ApiModels.ResultMdl
{
    public class EstateMetaDataResMdl
    {
        public EstateMetaDataResMdl()
        {
            ResultBaseMdl = new ResultBaseModel();
            UeEstateTypeList = new List<UeEstateType>();
            UeFlatTypeList = new List<UeFlatType>();
            UeEstatePartTypeList = new List<UeEstatePartType>();
            UeFurnitureGroupTypeList = new List<UeFurnitureGroupType>();
            UeFurnitureTypeList = new List<UeFurnitureType>();
            EPartTypeFrnGrpTypeList = new List<EPartTypeFrnGrpType>();
            EstateTypeEPartTypeList = new List<EstateTypeEPartType>();
        }

        public ResultBaseModel ResultBaseMdl { get; set; }
        public List<UeEstateType> UeEstateTypeList { get; set; }

        public List<UeFlatType> UeFlatTypeList { get; set; }
        public List<UeEstatePartType> UeEstatePartTypeList { get; set; }

        public List<UeFurnitureGroupType> UeFurnitureGroupTypeList { get; set; }
        public List<UeFurnitureType> UeFurnitureTypeList { get; set; }
        public List<EPartTypeFrnGrpType> EPartTypeFrnGrpTypeList { get; set; }
        public List<EstateTypeEPartType> EstateTypeEPartTypeList { get; set; }

    }
}
