using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DocumentModels;
using OAK.ServiceContracts;
using System.Collections.Generic;

namespace OAK.Services
{
    using OAK.Data;
    using OAK.Data.Paging;

    public class DocumentService : IDocumentService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public DocumentService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        #region Document
        public IPaginate<Document> GetAllDocuments(int index, int size)
        {
            IPaginate<Document> items = UnitOfWork.GetReadOnlyRepository<Document>().GetList(index: index, size: size);
            return items;
        }

        public bool AddDocument(Document document)
        {

            UnitOfWork.GetRepository<Document>().Add(document);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateDocument(Document document)
        {
            Document oldRecord = UnitOfWork.GetRepository<Document>().Single(x => x.Id == document.Id);


            //map
            oldRecord.DocumentTypeId = document.DocumentTypeId;

            UnitOfWork.GetRepository<Document>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Document GetDocument(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Document>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteDocument(int Id)
        {
            Document record = UnitOfWork.GetRepository<Document>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Document>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion Document

        #region DocumentType
        public IPaginate<DocumentType> GetAllDocumentTypes(int index, int size)
        {
            IPaginate<DocumentType> items = UnitOfWork.GetReadOnlyRepository<DocumentType>().GetList(index: index, size: size);
            return items;
        }

        public List<DocumentType> GetAllDocumentTypesList()
        {
            List<DocumentType> items = UnitOfWork.GetReadOnlyRepository<DocumentType>().GetAllReadOnly();
            return items;
        }

        public bool AddDocumentType(DocumentType documentType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(documentType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<DocumentType>().Add(documentType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateDocumentType(DocumentType documentType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(documentType.LocalKey, languageIdTexts);

            DocumentType oldRecord = UnitOfWork.GetRepository<DocumentType>().Single(x => x.Id == documentType.Id);


            //map
            //oldRecord.DocumentTypeId = estate.DocumentTypeId;

            UnitOfWork.GetRepository<DocumentType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;

        }
        public DocumentType GetDocumentType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<DocumentType>().Single(predicate: x => x.Id == id);

        }
        public bool DeleteDocumentType(int Id)
        {
            DocumentType record = UnitOfWork.GetRepository<DocumentType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<DocumentType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion DocumentType

    }
}
