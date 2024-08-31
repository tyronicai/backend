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
    using OAK.Model.BusinessModels.DocumentModels;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.Model.ViewModels.DocumentModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;

    [Route("api/{culture}/Document")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IDocumentService _documentService;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DocumentController(IDocumentService documentService,
            ILocalizationService localizationService,
            IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer,
            ILogger<DocumentController> logger, IMapper mapper)
        {
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _documentService = documentService;
            _localizationService = localizationService;
            _logger = logger;
            _mapper = mapper;
        }

        [Route("ShowMeTheCulture")]
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }

        [AllowAnonymous]
        [HttpPost("GetAllDocumentTypes")]
        public DocumentTypeListResMdl GetAllDocumentTypes()
        {
            DocumentTypeListResMdl furnitureTypeListResMdl = new DocumentTypeListResMdl();

            List<DocumentType> DocumentTypes = _documentService.GetAllDocumentTypesList();
            UeDocumentType ueDocumentType;
            foreach (var furnitureType in DocumentTypes)
            {

                ueDocumentType = _mapper.Map<UeDocumentType>(furnitureType);
                ueDocumentType.LanguageIdTexts = _localizationService.GetAllIdTexts(ueDocumentType.LocalKey);
                furnitureTypeListResMdl.UeDocumentTypeList.Add(ueDocumentType);
            }
            return furnitureTypeListResMdl;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddDocumentType")]
        public bool AddDocumentType(DocumentTypeReqMdl documentTypeAddReqMdl)
        {
            return _documentService.AddDocumentType(documentTypeAddReqMdl.DocumentType, documentTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateDocumentType")]
        public bool UpdateDocumentType(DocumentTypeReqMdl documentTypeAddReqMdl)
        {
            return _documentService.UpdateDocumentType(documentTypeAddReqMdl.DocumentType, documentTypeAddReqMdl.LanguageIdTexts);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetDocumentType")]
        public DocumentType GetDocumentType(int id)
        {
            return _documentService.GetDocumentType(id);

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteDocumentType")]
        public bool DeleteDocumentType(int id)
        {
            return _documentService.DeleteDocumentType(id);

        }
    }
}
