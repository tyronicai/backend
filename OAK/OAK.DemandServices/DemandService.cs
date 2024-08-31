using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MimeKit;
using OAK.Data;
using OAK.Data.Paging;
using OAK.Model.ApiModels.RequestMdl;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAK.Services
{
    public class DemandService : IDemandService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }
        private readonly IMapper _mapper;
        public IEmailService EmailService { get; }


        public DemandService(IUnitOfWork unitOfWork, ILocalizationService localizationService, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
            _mapper = mapper;
        }

        public DemandService(
            IUnitOfWork unitOfWork,
            ILocalizationService localizationService,
            IGenericAddressService genericAddressService,
            INotificationService notificationService,
            IMapper mapper,
            IEmailService emailService
        )
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
            _mapper = mapper;
            EmailService = emailService;
        }

        #region Demand
        public IPaginate<Demand> GetAllDemands(int index, int size)
        {
            IPaginate<Demand> items = UnitOfWork.GetReadOnlyRepository<Demand>().GetList(index: index, size: size);
            return items;
        }

        public IPaginate<Demand> GetAllDemands()
        {
            IPaginate<Demand> items = UnitOfWork.GetReadOnlyRepository<Demand>().GetList();
            return items;
        }

        public IList<Demand> GetDemandsByAccountId(int accountId, int demandStatusTypeId)
        {

            var demandIds = UnitOfWork.GetRepository<Demand>().GetAllReadOnly(x => x.AccountId == accountId && x.DemandStatusTypeId == demandStatusTypeId).ToList();

            foreach (var demandRec in demandIds)
            {
                var myTrs = (UnitOfWork.GetRepository<Transportation>()
                    .GetQueryable(x => x.DemandId == demandRec.Id,
                        include: source => source
                            .Include(x => x.FromAddress).ThenInclude(x => x.Country)
                            .Include(x => x.ToAddress).ThenInclude(x => x.Country)
                            .Include(x => x.FromEstate)
                            .Include(x => x.ToEstate)
                            .Include(x => x.Demand).ThenInclude(x => x.CompanyDemandServices).ThenInclude(x => x.Company))).ToList();
                demandRec.Transportations = myTrs;
            }
            return demandIds;
        }

        public IList<Transportation> GetTransportationsOfDemandsByAccountId(int accountId, bool processable = true)
        {

            List<Demand> demandIds;
            List<Transportation> transportationList = new List<Transportation>();

            try
            {
                if (processable)
                {
                    demandIds = UnitOfWork.GetRepository<Demand>()
                        .GetAllReadOnly(x => x.AccountId == accountId && x.DemandStatusTypeId <= 3).ToList();

                }
                else
                {
                    demandIds = UnitOfWork.GetRepository<Demand>()
                        .GetAllReadOnly(x => x.AccountId == accountId && x.DemandStatusTypeId > 3).ToList();

                }

                foreach (var demandRec in demandIds)
                {
                    var myTrs = UnitOfWork.GetRepository<Transportation>()
                        .GetQueryable(x => x.DemandId == demandRec.Id,
                            include: source => source
                                .Include(x => x.FromAddress).ThenInclude(x => x.Country)
                                .Include(x => x.ToAddress).ThenInclude(x => x.Country)
                                .Include(x => x.FromEstate).ThenInclude(x => x.EstateType)
                                .Include(x => x.ToEstate).ThenInclude(x => x.EstateType)
                                .Include(x => x.Demand).ThenInclude(x => x.CompanyDemandServices).ThenInclude(x => x.Company)
                                .Include(x => x.Demand.DemandStatusType)
                        ).ToList();

                    transportationList.AddRange(myTrs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return transportationList;
        }

        public bool AddDemand(Demand demand)
        {
            int hashCode = UnitOfWork.GetHashCode();
            UnitOfWork.GetRepository<Demand>().Add(demand);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateDemand(Demand demand)
        {
            int hashCode = UnitOfWork.GetHashCode();
            Demand oldRecord = UnitOfWork.GetRepository<Demand>().Single(x => x.Id == demand.Id);


            //map
            oldRecord = _mapper.Map<Demand>(demand);

            UnitOfWork.GetRepository<Demand>().Update(oldRecord.Id, oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Demand GetDemand(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Demand>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteDemand(int Id)
        {
            Demand record = UnitOfWork.GetRepository<Demand>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Demand>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Demand AcceptOffer(AcceptOfferReqMdl acceptOfferReqMdl, IDbContextTransaction trans = null)
        {
            var repo = UnitOfWork.GetRepository<Demand>();
            var companyDemandServiceRepo = UnitOfWork.GetRepository<CompanyDemandService>();
            var companyRepo = UnitOfWork.GetRepository<Company>();

            Demand demand = null;
            CompanyDemandService companyDemandService = null;
            Company company = null;
            bool localTrans = false;
            bool errorOccured = false;
            if (repo != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    companyDemandService = companyDemandServiceRepo.Single(x => x.Id == acceptOfferReqMdl.CompanyDemandServiceId);


                    demand = repo.Single(x => x.Id == acceptOfferReqMdl.DemandId);
                    demand.DemandContractValue = companyDemandService.OfferAmount;
                    demand.AcceptedOfferId = acceptOfferReqMdl.CompanyDemandServiceId;
                    demand.DemandStatusTypeId = 3;
                    company = companyRepo.Single(x => x.Id == companyDemandService.CompanyId);
                    repo.Update(demand);

                    MimeMessage mail = new MimeMessage();
                    mail.To.Add(new MailboxAddress(company.Email, ""));
                    mail.Subject = "Teklifiniz kabul edildi!";
                    mail.Body = new TextPart("plain")
                    {
                        Text = string.Format("demand id: #{0} \n offered amount: {1}", demand.Id,
                            companyDemandService.OfferAmount)
                    };
                    EmailService.Send(mail);



                    UnitOfWork.SaveChanges();
                }
                catch (Exception)
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
            return demand;
        }

        #endregion Demand

        #region DemandType
        public IPaginate<DemandType> GetAllDemandTypes(int index, int size)
        {
            IPaginate<DemandType> items = UnitOfWork.GetReadOnlyRepository<DemandType>().GetList(index: index, size: size);
            return items;
        }

        public List<DemandType> GetAllDemandTypesList()
        {
            List<DemandType> items = UnitOfWork.GetReadOnlyRepository<DemandType>().GetAllReadOnly();
            return items;
        }

        public bool AddDemandType(DemandType demandType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(demandType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<DemandType>().Add(demandType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateDemandType(DemandType demandType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(demandType.LocalKey, languageIdTexts);

            DemandType oldRecord = UnitOfWork.GetRepository<DemandType>().Single(x => x.Id == demandType.Id);


            //map
            //oldRecord.DemandTypeId = demand.DemandTypeId;

            UnitOfWork.GetRepository<DemandType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;

        }
        public DemandType GetDemandType(int id)
        {
            return UnitOfWork.GetRepository<DemandType>().Single(predicate: x => x.Id == id);

        }
        public bool DeleteDemandType(int Id)
        {
            DemandType record = UnitOfWork.GetRepository<DemandType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<DemandType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion DemandType

        #region DemandComment
        public IPaginate<DemandComment> GetAllDemandComments(int index, int size)
        {
            IPaginate<DemandComment> items = UnitOfWork.GetReadOnlyRepository<DemandComment>().GetList(index: index, size: size);
            return items;
        }


        public bool AddDemandComment(DemandComment demandComment)
        {

            UnitOfWork.GetRepository<DemandComment>().Add(demandComment);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateDemandComment(DemandComment demandComment)
        {
            DemandComment oldRecord = UnitOfWork.GetRepository<DemandComment>().Single(x => x.Id == demandComment.Id);


            //map
            //oldRecord.DemandTypeId = estate.DemandTypeId;

            UnitOfWork.GetRepository<DemandComment>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public DemandComment GetDemandComment(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<DemandComment>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteDemandComment(int Id)
        {
            DemandComment record = UnitOfWork.GetRepository<DemandComment>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<DemandComment>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion DemandComment

        #region DemandStatusType
        public IPaginate<DemandStatusType> GetAllDemandStatusTypes(int index, int size)
        {
            IPaginate<DemandStatusType> items = UnitOfWork.GetReadOnlyRepository<DemandStatusType>().GetList(index: index, size: size);
            return items;
        }

        public List<DemandStatusType> GetAllDemandStatusTypesList()
        {
            List<DemandStatusType> items = UnitOfWork.GetReadOnlyRepository<DemandStatusType>().GetAllReadOnly();
            return items;
        }

        public bool AddDemandStatusType(DemandStatusType demandStatusType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(demandStatusType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<DemandStatusType>().Add(demandStatusType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;
        }

        public bool UpdateDemandStatusType(DemandStatusType demandStatusType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(demandStatusType.LocalKey, languageIdTexts);
            DemandStatusType oldRecord = UnitOfWork.GetRepository<DemandStatusType>().Single(x => x.Id == demandStatusType.Id);


            //map
            //oldRecord.DemandTypeId = estate.DemandTypeId;

            UnitOfWork.GetRepository<DemandStatusType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public DemandStatusType GetDemandStatusType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<DemandStatusType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteDemandStatusType(int Id)
        {
            DemandStatusType record = UnitOfWork.GetRepository<DemandStatusType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<DemandStatusType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        #endregion DemandStatusType


    }
}
