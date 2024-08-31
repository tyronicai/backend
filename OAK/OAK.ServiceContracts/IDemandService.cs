using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Data;
using OAK.Data.Paging;
using System.Collections.Generic;

namespace OAK.ServiceContracts
{
    public interface IDemandService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        #region Demand
        IPaginate<Demand> GetAllDemands(int index, int size);
        IPaginate<Demand> GetAllDemands();
        IList<Demand> GetDemandsByAccountId(int accountId, int demandStatusTypeId);
        IList<Transportation> GetTransportationsOfDemandsByAccountId(int accountId, bool processable = true);
        bool AddDemand(Demand demand);
        bool UpdateDemand(Demand demand);
        Demand GetDemand(int id);
        bool DeleteDemand(int Id);
        Demand AcceptOffer(AcceptOfferReqMdl acceptOfferReqMdl, IDbContextTransaction trans = null);
        #endregion Demand

        #region DemandType
        IPaginate<DemandType> GetAllDemandTypes(int index, int size);
        List<DemandType> GetAllDemandTypesList();
        bool AddDemandType(DemandType demandType, List<LanguageIdText> languageIdTexts);
        bool UpdateDemandType(DemandType demandType, List<LanguageIdText> languageIdTexts);
        DemandType GetDemandType(int id);
        bool DeleteDemandType(int Id);
        #endregion DemandType

        #region DemandComment
        IPaginate<DemandComment> GetAllDemandComments(int index, int size);
        bool AddDemandComment(DemandComment demandComment);
        bool UpdateDemandComment(DemandComment demandComment);
        DemandComment GetDemandComment(int id);
        bool DeleteDemandComment(int Id);
        #endregion DemandComment

        #region DemandStatusType
        IPaginate<DemandStatusType> GetAllDemandStatusTypes(int index, int size);
        List<DemandStatusType> GetAllDemandStatusTypesList();
        bool AddDemandStatusType(DemandStatusType demandStatusType, List<LanguageIdText> languageIdTexts);
        bool UpdateDemandStatusType(DemandStatusType demandStatusType, List<LanguageIdText> languageIdTexts);
        DemandStatusType GetDemandStatusType(int id);
        bool DeleteDemandStatusType(int Id);
        #endregion DemandStatusType
    }


}
