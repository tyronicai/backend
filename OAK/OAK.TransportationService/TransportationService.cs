using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.TransportationModels;
using OAK.Model.BusinessModels.TransportationModels.TransportationCalculationModels;
using OAK.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OAK.TansportationServices
{
    using OAK.Data;
    using OAK.Data.Paging;

    public class TransportationService : ITransportationService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }
        private readonly IMapper _mapper;

        public TransportationService(IUnitOfWork unitOfWork, ILocalizationService localizationService, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
            _mapper = mapper;
        }

        #region Transportation

        public bool TransportationIdCompare(Transportation a, Transportation b)
        {
            return a.Id > b.Id;
        }

        public List<Transportation> GetAllTransportations(int argTransportationStatusTypeId)
        {
            IList<Transportation> items = UnitOfWork.GetReadOnlyRepository<Transportation>()
                .GetAllReadOnly(p => p.TransportationStatusTypeId == argTransportationStatusTypeId);

            var myItems = items.ToList().OrderBy(o => o.Id);


            return myItems.ToList();
        }

        public List<Transportation> GetAllTransportationsWithAddressesAndEstates(int argTransportationStatusTypeId)
        {
            IQueryable<Transportation> items = UnitOfWork.GetReadOnlyRepository<Transportation>()
                .GetQueryable(p => p.TransportationStatusTypeId == argTransportationStatusTypeId,
                    include: source => source
                        .Include(x => x.FromAddress)
                        .Include(x => x.ToAddress)
                        .Include(x => x.FromEstate)
                        .Include(x => x.ToEstate)
                        .Include(x => x.Demand));

            var myItems = items.ToList().OrderBy(o => o.Id);

            return myItems.ToList();

        }

        public IPaginate<Transportation> GetAllTransportations(int index, int size)
        {

            IPaginate<Transportation> items = UnitOfWork.GetReadOnlyRepository<Transportation>().GetList(index: index, size: size, orderBy: x => x.OrderByDescending(y => y.InitialTransportationDate));
            return items;
        }

        public bool AddTransportation(Transportation transportation)
        {

            UnitOfWork.GetRepository<Transportation>().Add(transportation);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public Transportation UpdateTransportation(Transportation transportation, IDbContextTransaction trans = null)
        {

            bool localTrans = null == trans;
            bool errorOccurred = false;
            var repo = UnitOfWork.GetRepository<Transportation>();
            Transportation newTransportation;

            if (transportation != null)
            {
                localTrans = null == trans;
                trans = UnitOfWork.BeginTransaction(trans);
                try
                {
                    newTransportation = repo.Single(predicate: x => x.Id == transportation.Id);
                    newTransportation = _mapper.Map<Transportation>(transportation);
                    newTransportation.Modified = DateTime.Now;
                    transportation = newTransportation;
                    repo.Update(newTransportation);
                    UnitOfWork.SaveChanges();
                }
                catch (Exception ex)
                {
                    errorOccurred = true;
                }
                if (localTrans)
                {
                    if (!errorOccurred)
                    {
                        UnitOfWork.CommitTransaction(trans);
                    }
                    else
                    {
                        UnitOfWork.RollbackTransaction(trans);
                    }
                }

            }
            return transportation;
        }

        public Transportation GetTransportation(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Transportation>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteTransportation(int Id)
        {
            Transportation record = UnitOfWork.GetRepository<Transportation>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Transportation>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion Transportation

        #region TransportationType
        public IPaginate<TransportationType> GetAllTransportationTypes(int index, int size)
        {
            IPaginate<TransportationType> items = UnitOfWork.GetReadOnlyRepository<TransportationType>().GetList(index: index, size: size);
            return items;
        }

        public bool AddTransportationType(TransportationType transportationType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(transportationType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<TransportationType>().Add(transportationType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateTransportationType(TransportationType transportationType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(transportationType.LocalKey, languageIdTexts);

            TransportationType oldRecord = UnitOfWork.GetRepository<TransportationType>().Single(x => x.Id == transportationType.Id);


            //map
            //oldRecord.TransportationTypeId = estate.TransportationTypeId;

            UnitOfWork.GetRepository<TransportationType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;

        }
        public TransportationType GetTransportationType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<TransportationType>().Single(predicate: x => x.Id == id);

        }
        public bool DeleteTransportationType(int Id)
        {
            TransportationType record = UnitOfWork.GetRepository<TransportationType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<TransportationType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion TransportationType

        #region TransportationStatusType
        public IPaginate<TransportationStatusType> GetAllTransportationStatusTypes(int index, int size)
        {
            IPaginate<TransportationStatusType> items = UnitOfWork.GetReadOnlyRepository<TransportationStatusType>().GetList(index: index, size: size);
            return items;
        }

        public bool AddTransportationStatusType(TransportationStatusType transportationStatusType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(transportationStatusType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<TransportationStatusType>().Add(transportationStatusType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateTransportationStatusType(TransportationStatusType transportationStatusType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(transportationStatusType.LocalKey, languageIdTexts);

            TransportationStatusType oldRecord = UnitOfWork.GetRepository<TransportationStatusType>().Single(x => x.Id == transportationStatusType.Id);


            //map
            //oldRecord.TransportationStatusTypeTypeId = transportationStatusType.TransportationStatusTypeTypeId;

            UnitOfWork.GetRepository<TransportationStatusType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public TransportationStatusType GetTransportationStatusType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<TransportationStatusType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteTransportationStatusType(int Id)
        {
            TransportationStatusType record = UnitOfWork.GetRepository<TransportationStatusType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<TransportationStatusType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion TransportationStatusType

        #region TransportationComment
        public IPaginate<TransportationComment> GetAllTransportationComments(int index, int size)
        {
            IPaginate<TransportationComment> items = UnitOfWork.GetReadOnlyRepository<TransportationComment>().GetList(index: index, size: size);
            return items;
        }

        public bool AddTransportationComment(TransportationComment transportationComment)
        {

            UnitOfWork.GetRepository<TransportationComment>().Add(transportationComment);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateTransportationComment(TransportationComment transportationComment)
        {
            TransportationComment oldRecord = UnitOfWork.GetRepository<TransportationComment>().Single(x => x.Id == transportationComment.Id);


            //map
            //oldRecord.TransportationCommentTypeId = transportationComment.TransportationCommentTypeId;

            UnitOfWork.GetRepository<TransportationComment>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public TransportationComment GetTransportationComment(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<TransportationComment>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteTransportationComment(int Id)
        {
            TransportationComment record = UnitOfWork.GetRepository<TransportationComment>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<TransportationComment>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion TransportationComment

        #region TransportationDocument
        public IPaginate<TransportationDocument> GetAllTransportationDocuments(int index, int size)
        {
            IPaginate<TransportationDocument> items = UnitOfWork.GetReadOnlyRepository<TransportationDocument>().GetList(index: index, size: size);
            return items;
        }

        public bool AddTransportationDocument(TransportationDocument transportationDocument)
        {

            UnitOfWork.GetRepository<TransportationDocument>().Add(transportationDocument);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateTransportationDocument(TransportationDocument transportationDocument)
        {
            TransportationDocument oldRecord = UnitOfWork.GetRepository<TransportationDocument>().Single(x => x.Id == transportationDocument.Id);


            //map
            //oldRecord.TransportationDocumentTypeId = transportationDocument.TransportationDocumentTypeId;

            UnitOfWork.GetRepository<TransportationDocument>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public TransportationDocument GetTransportationDocument(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<TransportationDocument>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteTransportationDocument(int Id)
        {
            TransportationDocument record = UnitOfWork.GetRepository<TransportationDocument>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<TransportationDocument>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion TransportationDocument



        public TransCalRes CalculateTransportationCostML(TransCalReq calculationVariables)
        {
            CostCalculation costCalculation = new CostCalculation();
            return costCalculation.CalculateTransportationML(calculationVariables);

        }
    }
}
