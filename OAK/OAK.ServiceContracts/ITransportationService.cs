using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels;
using System.Collections.Generic;

namespace OAK.ServiceContracts
{
    using OAK.Data;
    using OAK.Data.Paging;

    public interface ITransportationService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        List<Transportation> GetAllTransportations(int argTransportationStatusTypeId);
        List<Transportation> GetAllTransportationsWithAddressesAndEstates(int argTransportationStatusTypeId);
        IPaginate<Transportation> GetAllTransportations(int index, int size);
        bool AddTransportation(Transportation transportation);
        Transportation UpdateTransportation(Transportation transportation, IDbContextTransaction trans = null);
        Transportation GetTransportation(int id);
        bool DeleteTransportation(int Id);

        IPaginate<TransportationType> GetAllTransportationTypes(int index, int size);
        bool AddTransportationType(TransportationType transportationType, List<LanguageIdText> languageIdTexts);
        bool UpdateTransportationType(TransportationType transportationType, List<LanguageIdText> languageIdTexts);
        TransportationType GetTransportationType(int id);
        bool DeleteTransportationType(int Id);

        IPaginate<TransportationStatusType> GetAllTransportationStatusTypes(int index, int size);
        bool AddTransportationStatusType(TransportationStatusType transportationStatusType, List<LanguageIdText> languageIdTexts);
        bool UpdateTransportationStatusType(TransportationStatusType transportationStatusType, List<LanguageIdText> languageIdTexts);
        TransportationStatusType GetTransportationStatusType(int id);
        bool DeleteTransportationStatusType(int Id);

        IPaginate<TransportationComment> GetAllTransportationComments(int index, int size);
        bool AddTransportationComment(TransportationComment transportationComment);
        bool UpdateTransportationComment(TransportationComment transportationComment);
        TransportationComment GetTransportationComment(int id);
        bool DeleteTransportationComment(int Id);

        IPaginate<TransportationDocument> GetAllTransportationDocuments(int index, int size);
        bool AddTransportationDocument(TransportationDocument transportationDocument);
        bool UpdateTransportationDocument(TransportationDocument transportationDocument);
        TransportationDocument GetTransportationDocument(int id);
        bool DeleteTransportationDocument(int Id);

        TransCalRes CalculateTransportationCostML(TransCalReq calculationVariables);
    }

}
