using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CommentModels;
using System.Collections.Generic;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.ServiceContracts
{
    public interface ICommentService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        IPaginate<Comment> GetAllComments(int index, int size);
        bool AddComment(Comment comment);
        bool UpdateComment(Comment comment);
        Comment GetComment(int id);
        bool DeleteComment(int Id);

        IPaginate<CommentType> GetAllCommentTypes(int index, int size);
        List<CommentType> GetAllCommentTypesList();
        bool AddCommentType(CommentType commentType, List<LanguageIdText> languageIdTexts);
        bool UpdateCommentType(CommentType commentType, List<LanguageIdText> languageIdTexts);
        CommentType GetCommentType(int id);
        bool DeleteCommentType(int Id);

        IPaginate<CommentStatusType> GetAllCommentStatusTypes(int index, int size);
        List<CommentStatusType> GetAllCommentStatusTypesList();
        bool AddCommentStatusType(CommentStatusType commentStatusType, List<LanguageIdText> languageIdTexts);
        bool UpdateCommentStatusType(CommentStatusType commentStatusType, List<LanguageIdText> languageIdTexts);
        CommentStatusType GetCommentStatusType(int id);
        bool DeleteCommentStatusType(int Id);

    }

}
