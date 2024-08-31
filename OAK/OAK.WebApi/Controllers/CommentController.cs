using Microsoft.AspNetCore.Cors;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.ApiModels.RequestMdl;
    using OAK.Model.ApiModels.ResultMdl;
    using OAK.Model.BusinessModels.CommentModels;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.Model.ViewModels.CommentModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;

    [Route("api/{culture}/Comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ICommentService _commentService;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public CommentController(ICommentService commentService,
            ILocalizationService localizationService,
            ILogger<CommentController> logger, IMapper mapper, IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _commentService = commentService;
            _localizationService = localizationService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("ShowMeTheCulture")]
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }

        #region Comment
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllComments")]
        public List<Comment> GetAllComments(int index, int size)
        {

            List<Comment> commentList = _commentService.GetAllComments(index, size).Items as List<Comment>;

            return commentList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddComment")]
        public bool AddComment(Comment comment)
        {

            return _commentService.AddComment(comment);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateComment")]
        public bool UpdateComment(Comment comment)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetComment")]
        public Comment GetComment(int id)
        {
            return _commentService.GetComment(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteComment")]
        public bool DeleteComment(int id)
        {
            return _commentService.DeleteComment(id);
        }
        #endregion Comment

        #region CommentType
        [AllowAnonymous]
        [Route("GetAllCommentTypes")]
        public CommentTypeListResMdl GetAllCommentTypes()
        {
            CommentTypeListResMdl commentTypeListResMdl = new CommentTypeListResMdl();

            List<CommentType> CommentTypes = _commentService.GetAllCommentTypesList();
            UeCommentType ueCommentType;
            foreach (var furnitureType in CommentTypes)
            {

                ueCommentType = _mapper.Map<UeCommentType>(furnitureType);
                ueCommentType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueCommentType.LocalKey);
                commentTypeListResMdl.UeCommentTypeList.Add(ueCommentType);
            }
            return commentTypeListResMdl;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCommentType")]
        public bool AddCommentType(CommentTypeReqMdl CommentTypeAddReqMdl)
        {
            return _commentService.AddCommentType(CommentTypeAddReqMdl.CommentType, CommentTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCommentType")]
        public bool UpdateCommentType(CommentTypeReqMdl commentTypeAddReqMdl)
        {
            return _commentService.UpdateCommentType(commentTypeAddReqMdl.CommentType, commentTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCommentType")]
        public CommentType GetCommentType(int id)
        {
            return _commentService.GetCommentType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCommentType")]
        public bool DeleteCommentType(int id)
        {
            return _commentService.DeleteCommentType(id);

        }
        #endregion CommentType

        #region CommentStatusType
        [AllowAnonymous]
        [Route("GetAllCommentStatusTypes")]
        public CommentStatusTypeListResMdl GetAllCommentStatusTypes()
        {
            CommentStatusTypeListResMdl resultMdl = new CommentStatusTypeListResMdl();

            List<CommentStatusType> CommentStatusTypes = _commentService.GetAllCommentStatusTypesList();
            UeCommentStatusType ueCommentStatusType;
            foreach (var furnitureType in CommentStatusTypes)
            {

                ueCommentStatusType = _mapper.Map<UeCommentStatusType>(furnitureType);
                ueCommentStatusType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueCommentStatusType.LocalKey);
                resultMdl.UeCommentStatusTypeList.Add(ueCommentStatusType);
            }
            return resultMdl;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCommentStatusType")]
        public bool AddCommentStatusType(CommentStatusTypeReqMdl commentStatusTypeAddReqMdl)
        {
            return _commentService.AddCommentStatusType(commentStatusTypeAddReqMdl.CommentStatusType, commentStatusTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCommentStatusType")]
        public bool UpdateCommentStatusType(CommentStatusTypeReqMdl commentStatusTypeAddReqMdl)
        {
            return _commentService.UpdateCommentStatusType(commentStatusTypeAddReqMdl.CommentStatusType, commentStatusTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCommentStatusType")]
        public CommentStatusType GetCommentStatusType(int id)
        {
            return _commentService.GetCommentStatusType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCommentStatusType")]
        public bool DeleteCommentStatusType(int id)
        {
            return _commentService.DeleteCommentStatusType(id);

        }
        #endregion CommentStatusType
    }
}
