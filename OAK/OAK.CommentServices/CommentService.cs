using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.CommentModels;
using OAK.ServiceContracts;
using System.Collections.Generic;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.Services
{
    public class CommentService : ICommentService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public CommentService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        #region Comment
        public IPaginate<Comment> GetAllComments(int index, int size)
        {
            IPaginate<Comment> items = UnitOfWork.GetReadOnlyRepository<Comment>().GetList(index: index, size: size);
            return items;
        }

        public bool AddComment(Comment comment)
        {

            UnitOfWork.GetRepository<Comment>().Add(comment);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateComment(Comment comment)
        {
            Comment oldRecord = UnitOfWork.GetRepository<Comment>().Single(x => x.Id == comment.Id);


            //map
            oldRecord.CommentTypeId = comment.CommentTypeId;

            UnitOfWork.GetRepository<Comment>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Comment GetComment(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Comment>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteComment(int Id)
        {
            Comment record = UnitOfWork.GetRepository<Comment>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Comment>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion Comment

        #region CommentType
        public IPaginate<CommentType> GetAllCommentTypes(int index, int size)
        {
            IPaginate<CommentType> items = UnitOfWork.GetReadOnlyRepository<CommentType>().GetList(index: index, size: size);
            return items;
        }

        public List<CommentType> GetAllCommentTypesList()
        {
            List<CommentType> items = UnitOfWork.GetReadOnlyRepository<CommentType>().GetAllReadOnly();
            return items;
        }

        public bool AddCommentType(CommentType commentType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(commentType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<CommentType>().Add(commentType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateCommentType(CommentType commentType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(commentType.LocalKey, languageIdTexts);

            CommentType oldRecord = UnitOfWork.GetRepository<CommentType>().Single(x => x.Id == commentType.Id);


            //map
            //oldRecord.CommentTypeId = estate.CommentTypeId;

            UnitOfWork.GetRepository<CommentType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;

        }
        public CommentType GetCommentType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<CommentType>().Single(predicate: x => x.Id == id);

        }
        public bool DeleteCommentType(int Id)
        {
            CommentType record = UnitOfWork.GetRepository<CommentType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CommentType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion CommentType

        #region CommentStatusType
        public IPaginate<CommentStatusType> GetAllCommentStatusTypes(int index, int size)
        {
            IPaginate<CommentStatusType> items = UnitOfWork.GetReadOnlyRepository<CommentStatusType>().GetList(index: index, size: size);
            return items;
        }

        public List<CommentStatusType> GetAllCommentStatusTypesList()
        {
            List<CommentStatusType> items = UnitOfWork.GetReadOnlyRepository<CommentStatusType>().GetAllReadOnly();
            return items;
        }

        public bool AddCommentStatusType(CommentStatusType commentStatusType, List<LanguageIdText> languageIdTexts)
        {
            LocalizationService.ControlAndAdd(commentStatusType.LocalKey, languageIdTexts);

            UnitOfWork.GetRepository<CommentStatusType>().Add(commentStatusType);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateCommentStatusType(CommentStatusType commentStatusType, List<LanguageIdText> languageIdTexts)
        {

            LocalizationService.ControlAndAdd(commentStatusType.LocalKey, languageIdTexts);

            CommentStatusType oldRecord = UnitOfWork.GetRepository<CommentStatusType>().Single(x => x.Id == commentStatusType.Id);


            //map
            //oldRecord.CommentStatusTypeTypeId = commentStatusType.CommentStatusTypeTypeId;

            UnitOfWork.GetRepository<CommentStatusType>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public CommentStatusType GetCommentStatusType(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<CommentStatusType>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteCommentStatusType(int Id)
        {
            CommentStatusType record = UnitOfWork.GetRepository<CommentStatusType>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CommentStatusType>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion CommentStatusType

    }
}
