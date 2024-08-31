using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAK.CompanyServices
{
    using OAK.Model.ApiModels.RequestMdl;
    using OAK.Model.BusinessModels.CompanyModels;
    using OAK.ServiceContracts;
    using OAK.Validation.CompanyValidation;
    using OAK.Data;
    using OAK.Data.Paging;
    using OAK.Services;
    using OAK.Model.ApiModels.ResultMdl;
    using OAK.Model.BaseModels;
    using OAK.Model.BusinessModels.AddressModels;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.Model.BusinessModels.TransportationModels;
    using OAK.Model.Core;
    using OAK.Model.RequestModels;
    using OAK.Validation.CompanyValidation.Interfaces;


    public class CompanyService : ICompanyService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public INotificationService NotificaitonService { get; }
        public IGenericAddressService GenericAddressService { get; }

        public ICompanyValidator CompanyValidator { get; }
        public IAccountService AccountService { get; }

        private readonly IMapper _mapper;

        public IEmailService EmailService { get; }

        public CompanyService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
            CompanyValidator = new CompanyValidation();
        }

        public CompanyService(
            IUnitOfWork unitOfWork,
            ILocalizationService localizationService,
            ICompanyValidator companyValidator,
            IGenericAddressService genericAddressService,
            IAccountService accountService,
            INotificationService notificationService,
            IMapper mapper,
            IEmailService emailService
        )
        {
            UnitOfWork = unitOfWork;
            CompanyValidator = companyValidator;
            LocalizationService = localizationService;
            GenericAddressService = genericAddressService;
            AccountService = accountService;
            NotificaitonService = notificationService;
            _mapper = mapper;
            EmailService = emailService;
        }


        #region Company

        public IPaginate<Company> GetAllCompanies()
        {
            IPaginate<Company> companies = UnitOfWork.GetReadOnlyRepository<Company>().GetList();
            return companies;
        }

        public IPaginate<Company> GetAllCompaniesPaginate(int index, int size)
        {
            IPaginate<Company> items = UnitOfWork.GetReadOnlyRepository<Company>().GetList(index: index, size: size);
            return items;
        }

        public CreateCompanyResMdl AddCompany(CreateCompanyReqMdl createCompanyReqMdl, IDbContextTransaction trans = null)
        {
            CreateCompanyResMdl createCompanyResMdl = CompanyValidator.CreateCompanyValidation(createCompanyReqMdl);
            var repo = UnitOfWork.GetRepository<Company>();

            Company company;
            CompanyStatusType companyStatusType = null;
            GenericAddress genericAddress = null;

            bool localTrans = false;
            bool errorOccured = false;

            if (createCompanyResMdl.ResultBaseMdl.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    company = new Company();
                    genericAddress = new GenericAddress();
                    genericAddress.CountryId = createCompanyReqMdl.GenericAddress.CountryId;
                    genericAddress.Town = createCompanyReqMdl.GenericAddress.Town;
                    genericAddress.Street = createCompanyReqMdl.GenericAddress.Street;
                    genericAddress.HouseNumber = createCompanyReqMdl.GenericAddress.HouseNumber;
                    genericAddress.PostCode = createCompanyReqMdl.GenericAddress.PostCode;
                    genericAddress.PlaceName = createCompanyReqMdl.GenericAddress.PlaceName;

                    genericAddress.GenericAddressTypeId = 1;

                    genericAddress = GenericAddressService.AddGenericAddress(genericAddress, trans);

                    companyStatusType = GetCompanyStatusDefault(trans);

                    AccountService.MakeCompanyOwner(createCompanyReqMdl.Company.AccountId, trans);

                    company = createCompanyReqMdl.Company;
                    company.CompanyStatusTypeId = companyStatusType.Id;
                    company.GenericAddressId = genericAddress.Id;

                    repo.Add(company);
                    UnitOfWork.SaveChanges();
                    createCompanyResMdl.Company = company;
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return createCompanyResMdl;

        }

        public Company UpdateCompany(Company company, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Company>();
            Company newCompany;
            bool localTrans = false;
            bool errorOccured = false;

            if (company != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    newCompany = repo.Single(predicate: x => x.Id == company.Id);
                    newCompany = _mapper.Map<Company>(company);
                    newCompany.Modified = DateTime.Now;
                    company = newCompany;
                    repo.Update(newCompany);
                    UnitOfWork.SaveChanges();

                }
                catch (Exception ex)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return company;
        }

        public Company UpdateCompanyStatusType(UpdateCompanyStatusReqMdl companyStatusReqMdl, IDbContextTransaction trans = null)
        {
            Company company = null;
            Account account = null;
            CompanyStatusType companyStatusType = null;
            var repo = UnitOfWork.GetRepository<Company>();
            company = repo.Single(x => x.Id == companyStatusReqMdl.CompanyId);

            bool localTrans = false;
            bool errorOccured = false;
            if (companyStatusReqMdl.CompanyId > 0)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    if (null == company)
                    {
                        return null;
                    }
                    else
                    {
                        account = UnitOfWork.GetRepository<Account>().Single(x => x.Id == company.AccountId);
                        company.CompanyStatusTypeId = companyStatusReqMdl.NewStatusId;
                        companyStatusType = UnitOfWork.GetRepository<CompanyStatusType>().Single(x => x.Id == companyStatusReqMdl.NewStatusId);
                        repo.Update(company);
                        UnitOfWork.SaveChanges();
                        PushNotificationItem notificationItem = new PushNotificationItem();
                        notificationItem.Title = "Company Status";
                        notificationItem.Body = string.Format("Company Status updated. New Status: {0}!", companyStatusType.Name);
                        notificationItem.UserId = account.Username;
                        notificationItem.Token = account.FcmToken;
                        NotificationService.SendNotification(notificationItem).Wait();
                    }

                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }

            }

            return company;
        }

        public Company GetCompany(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Company>().Single(predicate: x => x.Id == id);
        }

        public GetCompanyByOwnerResMdl GetCompanyByOwner(GetCompanyByOwnerReqMdl byOwnerReqMdl)
        {
            GetCompanyByOwnerResMdl byOwnerResMdl = new GetCompanyByOwnerResMdl();
            int _id = int.Parse(byOwnerReqMdl.AccountId);

            byOwnerResMdl.Company = UnitOfWork.GetReadOnlyRepository<Company>().Single(x => x.AccountId == _id);
            byOwnerResMdl.CompanyStatusType = UnitOfWork.GetReadOnlyRepository<CompanyStatusType>().Single(x => x.Id == byOwnerResMdl.Company.CompanyStatusTypeId);
            if (byOwnerReqMdl.GetAddress)
            {
                byOwnerResMdl.GenericAddress = UnitOfWork.GetReadOnlyRepository<GenericAddress>().Single(x => x.Id == byOwnerResMdl.Company.GenericAddressId);
                byOwnerResMdl.Country = UnitOfWork.GetReadOnlyRepository<Country>().Single(x => x.Id == byOwnerResMdl.GenericAddress.CountryId);
            }
            return byOwnerResMdl;
        }

        public bool DeleteCompany(int Id)
        {
            Company record = UnitOfWork.GetRepository<Company>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Company>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion Company

        #region CompanyOfficialDocument
        public IPaginate<CompanyOfficialDocument> GetAllCompanyOfficialDocuments(int index, int size)
        {
            IPaginate<CompanyOfficialDocument> items = UnitOfWork.GetReadOnlyRepository<CompanyOfficialDocument>().GetList(index: index, size: size);
            return items;
        }

        public bool AddCompanyOfficialDocument(CompanyOfficialDocument companyOfficialDocument)
        {

            UnitOfWork.GetRepository<CompanyOfficialDocument>().Add(companyOfficialDocument);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateCompanyOfficialDocument(CompanyOfficialDocument companyOfficialDocument)
        {
            CompanyOfficialDocument oldRecord = UnitOfWork.GetRepository<CompanyOfficialDocument>().Single(x => x.Id == companyOfficialDocument.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<CompanyOfficialDocument>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public CompanyOfficialDocument GetCompanyOfficialDocument(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<CompanyOfficialDocument>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteCompanyOfficialDocument(int Id)
        {
            CompanyOfficialDocument record = UnitOfWork.GetRepository<CompanyOfficialDocument>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CompanyOfficialDocument>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion CompanyOfficialDocument

        #region CompanyPublicDocument
        public IPaginate<CompanyPublicDocument> GetAllCompanyPublicDocuments(int index, int size)
        {
            IPaginate<CompanyPublicDocument> items = UnitOfWork.GetReadOnlyRepository<CompanyPublicDocument>().GetList(index: index, size: size);
            return items;
        }


        public bool AddCompanyPublicDocument(CompanyPublicDocument companyService)
        {

            UnitOfWork.GetRepository<CompanyPublicDocument>().Add(companyService);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateCompanyPublicDocument(CompanyPublicDocument companyService)
        {
            CompanyPublicDocument oldRecord = UnitOfWork.GetRepository<CompanyPublicDocument>().Single(x => x.Id == companyService.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<CompanyPublicDocument>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public CompanyPublicDocument GetCompanyPublicDocument(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<CompanyPublicDocument>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteCompanyPublicDocument(int Id)
        {
            CompanyPublicDocument record = UnitOfWork.GetRepository<CompanyPublicDocument>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CompanyPublicDocument>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion CompanyPublicDocument

        #region CompanyDemandService
        public IPaginate<CompanyDemandService> GetAllCompanyDemandServices(int index, int size)
        {
            IPaginate<CompanyDemandService> items = UnitOfWork.GetReadOnlyRepository<CompanyDemandService>().GetList(index: index, size: size);
            return items;
        }

        public List<CompanyDemandService> GetAllCompanyDemandsServices(int companyDemandId = 0)
        {
            IOrderedEnumerable<CompanyDemandService> myItems = null;

            if (0 != companyDemandId)
            {
                IQueryable<CompanyDemandService> demands = UnitOfWork.GetRepository<CompanyDemandService>().GetQueryable(
                    predicate: x => x.DemandId == companyDemandId);
                myItems = demands.ToList().OrderBy(o => o.Id);
            }
            else
            {
                IQueryable<CompanyDemandService> demandServices = UnitOfWork.GetRepository<CompanyDemandService>().GetQueryable();
                myItems = demandServices.ToList().OrderBy(o => o.Id);
            }
            return myItems.ToList();
        }

        public List<Transportation> GetAllCompanyDemandServicesById(int companyId)
        {
            IOrderedEnumerable<CompanyDemandService> companyDemands = null;
            List<Transportation> myDemands = new List<Transportation>();
            var repo = UnitOfWork.GetRepository<CompanyDemandService>();
            IQueryable<CompanyDemandService> demands = repo.GetQueryable(predicate: x => x.CompanyId == companyId);
            companyDemands = demands.ToList().OrderBy(o => o.Id);
            foreach (var companyDemand in companyDemands)
            {
                IQueryable<Transportation> items = UnitOfWork.GetReadOnlyRepository<Transportation>()
                    .GetQueryable(p => p.Id == companyDemand.DemandId,
                        include: source => source
                            .Include(x => x.FromAddress)
                            .Include(x => x.ToAddress)
                            .Include(x => x.FromEstate)
                            .Include(x => x.ToEstate)
                            .Include(x => x.Demand));

                var myItems = items.ToList().OrderBy(o => o.Id);

                myDemands.AddRange(myItems);
            }
            return myDemands;
        }

        public RequestBaseModel AddCompanyDemandService(CompanyDemandService companyDemandService, IDbContextTransaction trans = null)
        {
            RequestBaseModel requestBaseModel = new RequestBaseModel();
            var repo = UnitOfWork.GetRepository<CompanyDemandService>();
            var isUpdated = false;
            bool localTrans = false;
            bool errorOccured = false;
            if (null != companyDemandService)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    var demand = UnitOfWork.GetRepository<Demand>().Single(x => x.Id == companyDemandService.DemandId);

                    if (demand.DemandStatusTypeId <= 2)
                    {
                        var existsOffer = repo.Single(x => x.DemandId == companyDemandService.DemandId && x.CompanyId == companyDemandService.CompanyId);
                        if (null == existsOffer)
                        {
                            repo.Add(companyDemandService);
                        }
                        else
                        {
                            existsOffer.OfferAmount = companyDemandService.OfferAmount;
                            repo.Update(existsOffer);
                            isUpdated = true;
                        }

                        if (demand.DemandMaxOfferedValue == 0 && demand.DemandMinOfferedValue == 0)
                        {
                            demand.DemandMaxOfferedValue = companyDemandService.OfferAmount;
                            demand.DemandMinOfferedValue = companyDemandService.OfferAmount;
                            demand.DemandAverageOfferedValue = (int)companyDemandService.OfferAmount;
                        }
                        else
                        {
                            var newAverage = (demand.DemandAverageOfferedValue + companyDemandService.OfferAmount) /
                                             (demand.DemandNumberOfOffers + 1);
                            demand.DemandAverageOfferedValue = (int)newAverage;
                        }

                        if (false == isUpdated)
                        {
                            demand.DemandNumberOfOffers += 1;
                        }

                        if (demand.DemandMinOfferedValue > companyDemandService.OfferAmount)
                        {
                            demand.DemandMinOfferedValue = companyDemandService.OfferAmount;
                        }

                        if (demand.DemandMaxOfferedValue < companyDemandService.OfferAmount)
                        {
                            demand.DemandMaxOfferedValue = companyDemandService.OfferAmount;
                        }

                        var account = UnitOfWork.GetRepository<Account>().Single(x => x.Id == demand.AccountId);
                        SendMailNewOffer(account, companyDemandService);

                        UnitOfWork.GetRepository<Demand>().Update(demand);
                        UnitOfWork.SaveChanges();
                    }
                    else
                    {
                        requestBaseModel.IsValid = false;
                        requestBaseModel.Description = "Already accepted!";
                    }

                }
                catch (Exception e)
                {
                    errorOccured = true;
                }


                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return requestBaseModel;
        }

        public List<CompanyDemandView> GetAllOfferedDemandsByCompanyId(int companyId, int demandStatusTypeId, IDbContextTransaction trans = null)
        {
            IOrderedEnumerable<CompanyDemandView> myItems = null;
            var repo = UnitOfWork.GetReadOnlyRepository<CompanyDemandView>();

            bool localTrans = false;
            bool errorOccured = false;

            if (null != repo)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    IQueryable<CompanyDemandView> myDemands =
                        repo.GetQueryable(x => x.CompanyId == companyId)
                            .Include(x => x.Demand)
                            .Include(x => x.Company);

                    myItems = myDemands.ToList().OrderBy(o => o.CompanyId);
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }

            return myItems?.ToList();
        }

        public bool UpdateCompanyDemandService(CompanyDemandService companyDemandService)
        {
            CompanyDemandService oldRecord = UnitOfWork.GetRepository<CompanyDemandService>().Single(x => x.Id == companyDemandService.Id);


            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<CompanyDemandService>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public CompanyDemandService GetCompanyDemandService(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<CompanyDemandService>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteCompanyDemandService(int Id)
        {
            CompanyDemandService record = UnitOfWork.GetRepository<CompanyDemandService>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CompanyDemandService>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public void SendMailNewOffer(Account account, CompanyDemandService companyDemandService)
        {
            MimeMessage mail = new MimeMessage();
            mail.To.Add(new MailboxAddress(account.Email,""));
            mail.Subject = "Yeni bir teklif var!";
            mail.Body = new TextPart("plain")
            {
                Text = string.Format("#{0} numarali demand \n{1} teklif edilen tutar \n{2} şirket id", companyDemandService.DemandId, companyDemandService.OfferAmount, companyDemandService.CompanyId)

            };
            EmailService.Send(mail);

        }

        #endregion CompanyDemandService

        #region CompanyStatusType

        public IPaginate<CompanyStatusType> GetAllCompanyStatusTypes()
        {
            IPaginate<CompanyStatusType> items = UnitOfWork.GetReadOnlyRepository<CompanyStatusType>().GetList();
            return items;
        }

        public IPaginate<CompanyStatusType> GetAllCompanyStatusType(int index, int size)
        {
            IPaginate<CompanyStatusType> items = UnitOfWork.GetReadOnlyRepository<CompanyStatusType>().GetList(index: index, size: size);
            return items;
        }

        public bool AddCompanyStatusType(CompanyStatusType companyStatusType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(companyStatusType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<CompanyStatusType>().Add(companyStatusType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateCompanyStatusType(CompanyStatusType companyStatusType)
        {
            CompanyStatusType oldRecord = UnitOfWork.GetRepository<CompanyStatusType>().Single(x => x.Id == companyStatusType.Id);
            //map
            //oldRecord.EstateTypeId = estate.EstateTypeId;

            UnitOfWork.GetRepository<CompanyStatusType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public CompanyStatusType GetCompanyStatusType(int id)
        {
            return UnitOfWork.GetRepository<CompanyStatusType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteCompanyStatusType(int Id)
        {
            CompanyStatusType record = UnitOfWork.GetRepository<CompanyStatusType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CompanyStatusType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        const string DefaultCompanyStatusName = "DefaultCompanyStatus";
        const string DefaultCompanyStatusDescription = "Default Company Status (Auto Added)";

        public CompanyStatusType GetCompanyStatusDefault(IDbContextTransaction trans = null)
        {
            bool localTrans = null == trans;

            trans = UnitOfWork.BeginTransaction(trans);

            var repo = UnitOfWork.GetRepository<CompanyStatusType>();
            CompanyStatusType defaultCompanyStatus = repo.Single(x => x.Id == 1);

            if (localTrans)
                UnitOfWork.CommitTransaction(trans);

            return defaultCompanyStatus;
        }

        #endregion CompanyStatusType

        #region CompanyPostCodeData

        public CreateCompanyPostCodeDateResMdl AddCompanyPostCodeData(CreateCompanyPostCodeDataReqMdl createCompanyPostCodeData, IDbContextTransaction trans = null)
        {
            CreateCompanyPostCodeDateResMdl createCompanyPostCodeDateResMdl = CompanyValidator.CreateCompanyPostCodeDataValidation(createCompanyPostCodeData);
            var repo = UnitOfWork.GetRepository<CompanyPostCodeData>();

            CompanyPostCodeData companyPostCodeData;

            bool localTrans = false;
            bool errorOccured = false;

            if (createCompanyPostCodeDateResMdl.ResultBaseMdl.IsValid)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);

                try
                {
                    companyPostCodeData = new CompanyPostCodeData();
                    companyPostCodeData.CompanyId = createCompanyPostCodeData.CompanyId;
                    companyPostCodeData.CountryId = createCompanyPostCodeData.CountryId;
                    companyPostCodeData.PostCode = createCompanyPostCodeData.PostCode;

                    repo.Add(companyPostCodeData);
                    UnitOfWork.SaveChanges();
                    createCompanyPostCodeDateResMdl.CompanyPostCodeData = companyPostCodeData;
                }
                catch (Exception e)
                {
                    errorOccured = true;
                }

                if (!errorOccured)
                {
                    if (localTrans)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                }
                else
                {
                    if (localTrans)
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }
            }
            return createCompanyPostCodeDateResMdl;
        }

        #endregion

    }
}
