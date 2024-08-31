using AutoMapper;
using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.BusinessModels.ParameterModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.Core;
using OAK.Model.ViewModels;
using OAK.Model.ViewModels.CommentModels;
using OAK.Model.ViewModels.CompanyModels;
using OAK.Model.ViewModels.CoreModels;
using OAK.Model.ViewModels.DemandModels;
using OAK.Model.ViewModels.EstateModels;
using OAK.Model.ViewModels.ParameterModels;
using OAK.Model.ViewModels.TransportationModels;

namespace OAK.Model.Mappings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Self Map

            CreateMap<Account, Account>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<GenericAddress, GenericAddress>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<GenericAddressType, GenericAddressType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<Comment, Comment>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<CommentType, CommentType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<CommentStatusType, CommentStatusType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<Company, Company>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<CompanyDemandService, CompanyDemandService>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<CompanyOfficialDocument, CompanyOfficialDocument>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<CompanyPublicDocument, CompanyPublicDocument>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<Demand, Demand>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<DemandOwner, DemandOwner>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<DemandType, DemandType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<DemandStatusType, DemandStatusType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<DemandComment, DemandComment>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<Estate, Estate>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EstateType, EstateType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EstatePart, EstatePart>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EstatePartType, EstatePartType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EstatePartFurniture, EstatePartFurniture>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EPartTypeFrnGrpType, EPartTypeFrnGrpType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EstateTypeEPartType, EstateTypeEPartType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<Furniture, Furniture>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<FurnitureType, FurnitureType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<CurrencyParameters, CurrencyParameters>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<Parameters, Parameters>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<Transportation, Transportation>()
                .ForMember(m => m.Id, opt => opt.UseDestinationValue())
                .ForMember(m => m.FromAddressId, opts => opts.MapFrom(src => src.FromAddress.Id))
                .ForMember(m => m.ToAddressId, opts => opts.MapFrom(src => src.ToAddress.Id))
                .ForMember(m => m.FromEstateId, opts => opts.MapFrom(src => src.FromEstate.Id))
                .ForMember(m => m.ToEstateId, opts => opts.MapFrom(src => src.ToEstate.Id))
                .ForMember(m => m.FromAddress, opts => opts.Ignore())
                .ForMember(m => m.ToAddress, opts => opts.Ignore())
                .ForMember(m => m.FromEstate, opts => opts.Ignore())
                .ForMember(m => m.ToEstate, opts => opts.Ignore());

            CreateMap<TransportationType, TransportationType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<TransportationStatusType, TransportationStatusType>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<TransportationComment, TransportationComment>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<TransportationDocument, TransportationDocument>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            CreateMap<Language, Language>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<Country, Country>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<PostCodeData, PostCodeData>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<SupportedPostCode, SupportedPostCode>().ForMember(m => m.Id, opt => opt.UseDestinationValue());
            CreateMap<EstatesFlat, EstatesFlat>().ForMember(m => m.Id, opt => opt.UseDestinationValue());

            #endregion Self Map

            CreateMap<UeGenericAddress, GenericAddress>();
            CreateMap<UeGenericAddressType, GenericAddressType>();
            CreateMap<UeComment, Comment>();
            CreateMap<UeCommentType, CommentType>();
            CreateMap<UeCommentStatusType, CommentStatusType>();

            CreateMap<UeCompany, Company>();
            CreateMap<UeCompanyStatusType, CompanyStatusType>();
            CreateMap<UeCompanyDemandService, CompanyDemandService>();
            CreateMap<UeCompanyOfficialDocument, CompanyOfficialDocument>();
            CreateMap<UeCompanyPublicDocument, CompanyPublicDocument>();

            CreateMap<UeDemand, Demand>();
            CreateMap<UeDemandOwner, DemandOwner>();

            CreateMap<UeDemandType, DemandType>();
            CreateMap<UeDemandStatusType, DemandStatusType>();
            CreateMap<UeDemandComment, DemandComment>();

            CreateMap<UeEstate, Estate>();
            CreateMap<UeEstateType, EstateType>();
            CreateMap<UeFlatType, FlatType>();
            CreateMap<UeEstatePart, EstatePart>();
            CreateMap<UeEstatePartType, EstatePartType>();
            CreateMap<UeEstatePartFurniture, EstatePartFurniture>();
            CreateMap<UeEPartTypeFrnGrpType, EPartTypeFrnGrpType>();
            CreateMap<UeEstateTypeEPartType, EstateTypeEPartType>();
            CreateMap<UeFurniture, Furniture>();
            CreateMap<UeFurnitureGroupType, FurnitureGroupType>();
            CreateMap<UeFurnitureType, FurnitureType>();

            CreateMap<UeCurrencyParameters, CurrencyParameters>();
            CreateMap<UeParameters, Parameters>();

            CreateMap<UeTransportation, Transportation>();
            CreateMap<UeTransportationType, TransportationType>();
            CreateMap<UeTransportationStatusType, TransportationStatusType>();
            CreateMap<UeTransportationComment, TransportationComment>();
            CreateMap<UeTransportationDocument, TransportationDocument>();

            CreateMap<UeLanguage, Language>();
            CreateMap<UeCountry, Country>();
            CreateMap<UePostCodeData, PostCodeData>();
            CreateMap<UeSupportedPostCode, SupportedPostCode>();
            CreateMap<UeEstatesFlat, EstatesFlat>();

            // Reverse maps 
            CreateMap<GenericAddress, UeGenericAddress>();
            CreateMap<GenericAddressType, UeGenericAddressType>();
            CreateMap<Comment, UeComment>();
            CreateMap<CommentType, UeCommentType>();
            CreateMap<CommentStatusType, UeCommentStatusType>();

            CreateMap<Company, UeCompany>();
            CreateMap<CompanyStatusType, UeCompanyStatusType>();
            CreateMap<CompanyDemandService, UeCompanyDemandService>();
            CreateMap<CompanyOfficialDocument, UeCompanyOfficialDocument>();
            CreateMap<CompanyPublicDocument, UeCompanyPublicDocument>();

            CreateMap<Demand, UeDemand>();
            CreateMap<DemandOwner, UeDemandOwner>();

            CreateMap<DemandType, UeDemandType>();
            CreateMap<DemandStatusType, UeDemandStatusType>();
            CreateMap<DemandComment, UeDemandComment>();

            CreateMap<Estate, UeEstate>();
            CreateMap<EstateType, UeEstateType>();
            CreateMap<FlatType, UeFlatType>();
            CreateMap<EstatePart, UeEstatePart>();
            CreateMap<EstatePartType, UeEstatePartType>();
            CreateMap<EstatePartFurniture, UeEstatePartFurniture>();
            CreateMap<EPartTypeFrnGrpType, UeEPartTypeFrnGrpType>();
            CreateMap<EstateTypeEPartType, UeEstateTypeEPartType>();
            CreateMap<Furniture, UeFurniture>();
            CreateMap<FurnitureType, UeFurnitureType>();
            CreateMap<FurnitureGroupType, UeFurnitureGroupType>();

            CreateMap<CurrencyParameters, UeCurrencyParameters>();
            CreateMap<Parameters, UeParameters>();

            CreateMap<Transportation, UeTransportation>();
            CreateMap<TransportationType, UeTransportationType>();
            CreateMap<TransportationStatusType, UeTransportationStatusType>();
            CreateMap<TransportationComment, UeTransportationComment>();
            CreateMap<TransportationDocument, UeTransportationDocument>();

            CreateMap<Language, UeLanguage>();
            CreateMap<Country, UeCountry>();
            CreateMap<PostCodeData, UePostCodeData>();
            CreateMap<SupportedPostCode, UeSupportedPostCode>();
            CreateMap<EstatesFlat, UeEstatesFlat>();
        }
    }
}
