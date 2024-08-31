using Microsoft.AspNetCore.Cors;
using System.IO;
using System.Text;

namespace OAK.WebApi.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging;
    using OAK.Model.ApiModels.RequestMdl;
    using OAK.Model.BusinessModels.CompanyModels;
    using OAK.Model.Localization;
    using OAK.Model.StaticModels;
    using OAK.ServiceContracts;
    using System.Collections.Generic;
    using System.Globalization;

    [Route("api/{culture}/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IStringLocalizer<ValuesController> _stringLocalizer;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        //ILanguageService

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger, IMapper mapper, IStringLocalizer<ValuesController> valuesLocalizerizer, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _companyService = companyService;
            _stringLocalizer = valuesLocalizerizer;
            _sharedLocalizer = sharedLocalizer;
            _logger = logger;
            _mapper = mapper;
        }

        #region Company
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllCompaniesPaginate")]
        public List<Company> GetAllCompaniesPaginate(PaginateReqMdl paginateReqMdl)
        {
            List<Company> companyList = _companyService.GetAllCompaniesPaginate(
                paginateReqMdl.index, paginateReqMdl.size).Items as List<Company>;
            return companyList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            var companies = _companyService.GetAllCompanies();
            return Ok(companies);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCompany")]
        public IActionResult AddCompany([FromBody] CreateCompanyReqMdl createCompanyReqMdl)
        {
            var companyResult = _companyService.AddCompany(createCompanyReqMdl);
            return Ok(companyResult);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCompany")]
        public IActionResult UpdateCompany(Company company)
        {
            var companyResult = _companyService.UpdateCompany(company);
            return Ok(companyResult);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCompanyStatus")]
        public IActionResult UpdateCompanyStatusType(UpdateCompanyStatusReqMdl companyStatusReqMdl)
        {
            var companyResult = _companyService.UpdateCompanyStatusType(companyStatusReqMdl);
            return Ok(companyResult);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCompany")]
        public Company GetCompany(int id)
        {
            return _companyService.GetCompany(id);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCompanyByOwner")]
        public IActionResult GetCompanyByOwner([FromBody] GetCompanyByOwnerReqMdl byOwnerReqMdl)
        {
            var companyResult = _companyService.GetCompanyByOwner(byOwnerReqMdl);
            return Ok(companyResult);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCompany")]
        public bool DeleteCompany(int id)
        {
            return _companyService.DeleteCompany(id);
        }
        #endregion Company

        #region CompanyOfficialDocument
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllCompanyOfficialDocuments")]
        public List<CompanyOfficialDocument> GetAllCompanyOfficialDocuments(int index, int size)
        {

            List<CompanyOfficialDocument> companyList = _companyService.GetAllCompanyOfficialDocuments(index, size).Items as List<CompanyOfficialDocument>;

            return companyList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCompanyOfficialDocument")]
        public bool AddCompanyOfficialDocuments(CompanyOfficialDocument companyOfficialDocument)
        {

            return _companyService.AddCompanyOfficialDocument(companyOfficialDocument);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCompanyOfficialDocument")]
        public bool UpdateCompanyOfficialDocument(CompanyOfficialDocument companyOfficialDocument)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCompanyOfficialDocument")]
        public CompanyOfficialDocument GetCompanyOfficialDocument(int id)
        {
            return _companyService.GetCompanyOfficialDocument(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCompanyOfficialDocument")]
        public bool DeleteCompanyOfficialDocument(int id)
        {
            return _companyService.DeleteCompanyOfficialDocument(id);
        }
        #endregion CompanyOfficialDocument

        #region CompanyPublicDocument
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllCompanyPublicDocuments")]
        public List<CompanyPublicDocument> GetAllCompanyPublicDocuments(int index, int size)
        {

            List<CompanyPublicDocument> companyList = _companyService.GetAllCompanyPublicDocuments(index, size).Items as List<CompanyPublicDocument>;

            return companyList;

        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCompanyPublicDocument")]
        public bool AddCompanyPublicDocuments(CompanyPublicDocument companyPublicDocument)
        {

            return _companyService.AddCompanyPublicDocument(companyPublicDocument);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCompanyPublicDocument")]
        public bool UpdateCompanyPublicDocument(CompanyPublicDocument companyPublicDocument)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCompanyPublicDocument")]
        public CompanyPublicDocument GetCompanyPublicDocument(int id)
        {
            return _companyService.GetCompanyPublicDocument(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCompanyPublicDocument")]
        public bool DeleteCompanyPublicDocument(int id)
        {
            return _companyService.DeleteCompanyPublicDocument(id);
        }
        #endregion CompanyPublicDocument

        #region CompanyDemandService
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllCompanyDemandServices")]
        public List<CompanyDemandService> GetAllCompanyDemandServices(int index, int size)
        {
            List<CompanyDemandService> companyList = _companyService.GetAllCompanyDemandServices(index, size).Items as List<CompanyDemandService>;
            return companyList;
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllCompanyDemandServicesById")]
        public IActionResult GetAllCompanyDemandServicesById([FromBody] CompanyDemandService companyDemandService)
        {
            var allOffers = _companyService.GetAllCompanyDemandsServices(companyDemandService.Id);
            return Ok(allOffers);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllOfferedDemandsByCompanyId")]
        public IActionResult GetAllOfferedDemandsByCompanyId([FromBody] CompanyDemandService companyDemandService)
        {
            var allOffers = _companyService.GetAllOfferedDemandsByCompanyId(companyDemandService.CompanyId, companyDemandService.DemandStatusTypeId);
            return Ok(allOffers);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetAllOfferedDemandsByCompanyIdTest")]
        public IActionResult GetAllOfferedDemandsByCompanyIdTest([FromBody] CompanyDemandService companyDemandService)
        {
            var allOffers = _companyService.GetAllCompanyDemandServicesById(companyDemandService.CompanyId);
            return Ok(allOffers);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCompanyDemandService")]
        public IActionResult AddCompanyDemandServices(CompanyDemandService companyDemandService)
        {
            var companyDemand = _companyService.AddCompanyDemandService(companyDemandService);
            return Ok(companyDemand);
        }

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("UpdateCompanyDemandService")]
        public bool UpdateCompanyDemandService(CompanyDemandService companyDemandService)
        {
            bool retVal = true;
            return retVal;
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("GetCompanyDemandService")]
        public CompanyDemandService GetCompanyDemandService(int id)
        {
            return _companyService.GetCompanyDemandService(id);
        }


        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("DeleteCompanyDemandService")]
        public bool DeleteCompanyDemandService(int id)
        {
            return _companyService.DeleteCompanyDemandService(id);
        }
        #endregion CompanyDemandService

        #region CompanyStatus

        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpGet("GetAllCompanyStatusTypes")]
        public IActionResult GetAllCompanyStatusType()
        {
            var companyStatusTypes = _companyService.GetAllCompanyStatusTypes();
            return Ok(companyStatusTypes);
        }

        #endregion CompanyStatus

        #region CompanyPostCodeData

        //[Authorize]
        [Authorize(Policy = AppStaticValues.RoleBasedPermissionName)]
        [HttpPost("AddCompanyPostCodeData")]
        public IActionResult AddCompanyPostCodeData([FromBody] CreateCompanyPostCodeDataReqMdl createCompanyPostCodeDataReqMdl)
        {
            var companyPostCodeData = _companyService.AddCompanyPostCodeData(createCompanyPostCodeDataReqMdl);
            return Ok(companyPostCodeData);
        }


        #endregion
    }
}
