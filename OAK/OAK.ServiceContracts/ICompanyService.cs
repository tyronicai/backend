using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.ApiModels.ResultMdl;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.Core;
using OAK.Model.RequestModels;
using System.Collections.Generic;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.ServiceContracts
{
    public interface ICompanyService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        #region Company

        IPaginate<Company> GetAllCompanies();
        IPaginate<Company> GetAllCompaniesPaginate(int index, int size);
        CreateCompanyResMdl AddCompany(CreateCompanyReqMdl createCompanyReqMdl, IDbContextTransaction trans = null);
        Company UpdateCompany(Company company, IDbContextTransaction trans = null);
        Company GetCompany(int id);
        Company UpdateCompanyStatusType(UpdateCompanyStatusReqMdl companyStatusReqMdl,
            IDbContextTransaction trans = null);
        GetCompanyByOwnerResMdl GetCompanyByOwner(GetCompanyByOwnerReqMdl byOwnerReqMdl);
        bool DeleteCompany(int Id);
        #endregion Company

        #region CompanyOfficialDocuments
        IPaginate<CompanyOfficialDocument> GetAllCompanyOfficialDocuments(int index, int size);
        bool AddCompanyOfficialDocument(CompanyOfficialDocument companyOfficialDocument);
        bool UpdateCompanyOfficialDocument(CompanyOfficialDocument companyOfficialDocument);
        CompanyOfficialDocument GetCompanyOfficialDocument(int id);
        bool DeleteCompanyOfficialDocument(int Id);
        #endregion CompanyOfficialDocuments

        #region CompanyPublicDocument
        IPaginate<CompanyPublicDocument> GetAllCompanyPublicDocuments(int index, int size);
        bool AddCompanyPublicDocument(CompanyPublicDocument companyPublicDocument);
        bool UpdateCompanyPublicDocument(CompanyPublicDocument companyPublicDocument);
        CompanyPublicDocument GetCompanyPublicDocument(int id);
        bool DeleteCompanyPublicDocument(int Id);
        #endregion CompanyPublicDocument

        #region CompanyDemandService
        IPaginate<CompanyDemandService> GetAllCompanyDemandServices(int index, int size);
        List<CompanyDemandService> GetAllCompanyDemandsServices(int companyDemandId = 0);
        RequestBaseModel AddCompanyDemandService(CompanyDemandService companyDemandService, IDbContextTransaction trans = null);
        List<CompanyDemandView> GetAllOfferedDemandsByCompanyId(int companyId, int demandStatusTypeId, IDbContextTransaction trans = null);
        List<Transportation> GetAllCompanyDemandServicesById(int companyId);
        bool UpdateCompanyDemandService(CompanyDemandService companyDemandService);
        CompanyDemandService GetCompanyDemandService(int id);
        bool DeleteCompanyDemandService(int Id);
        void SendMailNewOffer(Account account, CompanyDemandService companyDemandService);

        #endregion CompanyDemandService

        #region CompanyStatusType

        IPaginate<CompanyStatusType> GetAllCompanyStatusTypes();
        IPaginate<CompanyStatusType> GetAllCompanyStatusType(int index, int size);

        bool AddCompanyStatusType(CompanyStatusType companyStatusType, List<LanguageIdText> languageIdTexts);

        bool UpdateCompanyStatusType(CompanyStatusType companyStatusType);

        CompanyStatusType GetCompanyStatusType(int id);
        bool DeleteCompanyStatusType(int Id);

        CompanyStatusType GetCompanyStatusDefault(IDbContextTransaction trans = null);

        #endregion CompanyStatusType

        #region CompanyPostCodeData

        CreateCompanyPostCodeDateResMdl AddCompanyPostCodeData(CreateCompanyPostCodeDataReqMdl createCompanyPostCodeData, IDbContextTransaction trans = null);

        #endregion
    }

}
