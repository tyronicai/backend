using AutoMapper;
using OAK.Model.BusinessModels;
using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.BusinessModels.ParameterModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.ViewModels;
using OAK.Model.ViewModels.CommentModels;
using OAK.Model.ViewModels.CompanyModels;
using OAK.Model.ViewModels.DemandModels;
using OAK.Model.ViewModels.EstateModels;
using OAK.Model.ViewModels.ParameterModels;
using OAK.Model.ViewModels.TransportationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace OAK.Model.Mappings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UeGenericAddress, GenericAddress>();
            CreateMap<UeGenericAddressType, GenericAddressType>();
            CreateMap<UeComment, Comment>();
            CreateMap<UeCommentType, CommentType>();
            CreateMap<UeCommentStatusType, CommentStatusType>();

            CreateMap<UeCompany, Company>();
            CreateMap<UeCompanyDemandService, CompanyDemandService>();
            CreateMap<UeCompanyOfficialDocument, CompanyOfficialDocument>();
            CreateMap<UeCompanyPublicDocument, CompanyPublicDocument>();

            CreateMap<UeDemand, Demand>();
            CreateMap<UeDemandType, DemandType>();
            CreateMap<UeDemandStatusType, DemandStatusType>();
            CreateMap<UeDemandComment, DemandComment>();

            CreateMap<UeEstate, Estate>();
            CreateMap<UeEstateType, EstateType>();
            CreateMap<UeEstatePart, EstatePart>();
            CreateMap<UeEstatePartType, EstatePartType>();
            CreateMap<UeEstatePartFurniture, EstatePartFurniture>();

            CreateMap<UeFurniture, Furniture>();
            CreateMap<UeFurnitureType, FurnitureType>();

            CreateMap<UeCurrencyParameters, CurrencyParameters>();
            CreateMap<UeParameters, Parameters>();

            CreateMap<UeTransportation, Transportation>();
            CreateMap<UeTransportationType, TransportationType>();
            CreateMap<UeTransportationStatusType, TransportationStatusType>();
            CreateMap<UeTransportationComment, TransportationComment>();
            CreateMap<UeTransportationDocument, TransportationDocument>();
        }
    }
}
