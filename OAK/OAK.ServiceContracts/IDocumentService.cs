using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.DocumentModels;
using System.Collections.Generic;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.ServiceContracts
{
    public interface IDocumentService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        IPaginate<Document> GetAllDocuments(int index, int size);
        bool AddDocument(Document document);
        bool UpdateDocument(Document document);
        Document GetDocument(int id);
        bool DeleteDocument(int Id);

        IPaginate<DocumentType> GetAllDocumentTypes(int index, int size);
        List<DocumentType> GetAllDocumentTypesList();
        bool AddDocumentType(DocumentType documentType, List<LanguageIdText> languageIdTexts);
        bool UpdateDocumentType(DocumentType documentType, List<LanguageIdText> languageIdTexts);
        DocumentType GetDocumentType(int id);
        bool DeleteDocumentType(int Id);

    }

}
