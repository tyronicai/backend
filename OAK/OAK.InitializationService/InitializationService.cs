using Microsoft.EntityFrameworkCore.Storage;
using OAK.Model.BaseModels;
using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.CompanyModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.Core;
using OAK.ServiceContracts;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace OAK.InitializationServices
{
    using OAK.Data;
    using OAK.Data.Paging;

    public class InitializationService : IInitializationService
    {
        public readonly ILanguageService _languageService;
        public readonly IRoleService _roleService;
        public readonly IAccountService _accountService;
        public readonly IEstateService _estateService;
        public readonly ICountryService _countryService;
        public readonly IDemandService _demandService;
        public readonly ICompanyService _companyService;
        public readonly IGenericAddressService _genericAddressService;
        public IUnitOfWork _unitOfWork;
        public string _dataDocumentPath;
        private IConfiguration _configuration;
        public string[] cultureNameList = { "en-US", "de-DE", "fr-FR", "tr-TR", "es-ES", "ru-RU" };

        public Account adminAccount = new Account()
        {
            Username = "leventgenc",
            Password = "1234",
            Email = "leventsezgin.genc@gmail.com",
            FirstName = "Levent Sezgin",
            LastName = "Genç",
            LastPasswordChangeDate = DateTime.Now,
            LoginAttempts = 0,
            PhoneNumber = "+905425618343"
        };

        public InitializationService(
            ILanguageService languageService,
            IRoleService roleService,
            IAccountService accountService,
            IEstateService estateService,
            ICountryService countryService,
            IDemandService demandService,
            ICompanyService companyService,
            IGenericAddressService genericAddressService,
            IUnitOfWork unitOfWork,
            IConfiguration configuration
            )
        {
            _languageService = languageService;
            _roleService = roleService;
            _accountService = accountService;
            _estateService = estateService;
            _countryService = countryService;
            _demandService = demandService;
            _companyService = companyService;
            _genericAddressService = genericAddressService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _dataDocumentPath = _configuration.GetSection("DocumentSettings:DataDocumentPath").Value;

        }

        public bool IsDatabasePopulated(){
            bool res=true;
            try{
                var r=_estateService.GetAllEstateTypes(1,20);
                if (r.Count==0 || r==null)
                    res=false;
            }
            catch{
                
                    res=false;
            }
            
            return res;
        }
        public void AddAdminAccount()
        {

            using (IDbContextTransaction trans = _unitOfWork.BeginTransaction())
            {
                if (!_accountService.IsAccountAvailableByUserName(adminAccount.Username))
                {

                    _accountService.Register(adminAccount, trans);

                    var newRole = new Role { Name = "Admin", LocalKey = "Role.Name.Admin" };
                    _roleService.Add(newRole, trans);

                    var newAccountRole = new AccountRole { AccountId = adminAccount.Id, RoleId = newRole.Id };
                    _accountService.AddRoleToAccount(newAccountRole, trans);

                    _unitOfWork.CommitTransaction(trans);
                }

            }
            var auth = _accountService.Authenticate(new LoginModel()
            { Password = "1234", Email = "leventsezgin.genc@gmail.com" });
        }

        public void AddDefaultLanguages()
        {

            Language language1 = new Language()
            {
                LocalKey = "Language.en-US",
                Name = "English (United States)",
                CultureName = "en-US",
                IsActive = true
            };

            if (null == _languageService.GetLanguageByCultureName(language1.CultureName))
                _languageService.Add(language1);


            Language language2 = new Language()
            {
                LocalKey = "Language.de-DE",
                Name = "Deutsch",
                CultureName = "de-DE",
                IsActive = true
            };

            if (null == _languageService.GetLanguageByCultureName(language2.CultureName))
                _languageService.Add(language2);


            Language language3 = new Language()
            {
                LocalKey = "Language.fr-FR",
                Name = "Français",
                CultureName = "fr-FR",
                IsActive = false
            };

            if (null == _languageService.GetLanguageByCultureName(language3.CultureName))
                _languageService.Add(language3);


            Language language4 = new Language()
            {
                LocalKey = "Language.tr-TR",
                Name = "Türkçe (Türkiye)",
                CultureName = "tr-TR",
                IsActive = true
            };

            if (null == _languageService.GetLanguageByCultureName(language4.CultureName))
                _languageService.Add(language4);


            Language language5 = new Language()
            {
                LocalKey = "Language.es-ES",
                Name = "Español",
                CultureName = "es-ES",
                IsActive = false
            };

            if (null == _languageService.GetLanguageByCultureName(language5.CultureName))
                _languageService.Add(language5);


            Language language6 = new Language()
            {
                LocalKey = "Language.ru-RU",
                Name = "Pусский",
                CultureName = "ru-RU",
                IsActive = false
            };

            if (null == _languageService.GetLanguageByCultureName(language6.CultureName))
                _languageService.Add(language6);

        }

        public void AddLanguages()
        {

        }

        #region Estate

        public void AddEstateTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            EstateType estateType;

            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["EstateType"];
                //Set the cell value using row and column.
                for (int numberOfEstateTypes = 0; numberOfEstateTypes < ws.Cells[1, 1].GetValue<int>(); numberOfEstateTypes++)
                {
                    estateType = new EstateType()
                    {
                        Id = ws.Cells[4 + numberOfEstateTypes, 2].GetValue<int>(),
                        LocalKey = "EstateType.Id." + ws.Cells[4 + numberOfEstateTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfEstateTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfEstateTypes, 4].GetValue<string>()
                    };

                    if (null == _estateService.GetEstateType(estateType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfEstateTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfEstateTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _estateService.AddEstateType(estateType, LanguageIdTexts);
                    }
                }

            }
        }

        public void AddFlatTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            FlatType flatType;

            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["FlatType"];
                //Set the cell value using row and column.
                for (int numberOfFlatTypes = 0; numberOfFlatTypes < ws.Cells[1, 1].GetValue<int>(); numberOfFlatTypes++)
                {
                    flatType = new FlatType()
                    {
                        Id = ws.Cells[4 + numberOfFlatTypes, 2].GetValue<int>(),
                        LocalKey = "FlatType.Id." + ws.Cells[4 + numberOfFlatTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfFlatTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfFlatTypes, 4].GetValue<string>()
                    };

                    if (null == _estateService.GetFlatType(flatType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfFlatTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfFlatTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _estateService.AddFlatType(flatType, LanguageIdTexts);
                    }
                }

            }
        }
        public void AddEstatePartTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            EstatePartType estatePartType;
            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["EstatePartType"];
                //Set the cell value using row and column.
                for (int numberOfEstatePartTypes = 0; numberOfEstatePartTypes < ws.Cells[1, 1].GetValue<int>(); numberOfEstatePartTypes++)
                {
                    estatePartType = new EstatePartType()
                    {
                        Id = ws.Cells[4 + numberOfEstatePartTypes, 2].GetValue<int>(),
                        LocalKey = "EstatePartType.Id." + ws.Cells[4 + numberOfEstatePartTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfEstatePartTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfEstatePartTypes, 4].GetValue<string>(),
                        IsActive = true,
                        IsOuterPart = ws.Cells[4 + numberOfEstatePartTypes, 17].GetValue<string>() == "x" ? true : false
                    };

                    if (null == _estateService.GetEstatePartType(estatePartType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfEstatePartTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfEstatePartTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _estateService.AddEstatePartType(estatePartType, LanguageIdTexts);
                    }
                }

            }
        }

        public void AddFurnitureCalculationTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            FurnitureCalculationType furnitureCalculationType;
            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["FurnitureCalculationType"];
                //Set the cell value using row and column.
                for (int numberOfFurnitureCalculationTypes = 0; numberOfFurnitureCalculationTypes < ws.Cells[1, 1].GetValue<int>(); numberOfFurnitureCalculationTypes++)
                {
                    furnitureCalculationType = new FurnitureCalculationType()
                    {
                        Id = ws.Cells[4 + numberOfFurnitureCalculationTypes, 2].GetValue<int>(),
                        LocalKey = "FurnitureCalculationType.Id." + ws.Cells[4 + numberOfFurnitureCalculationTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfFurnitureCalculationTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfFurnitureCalculationTypes, 4].GetValue<string>()
                    };

                    if (null == _estateService.GetFurnitureCalculationType(furnitureCalculationType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfFurnitureCalculationTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfFurnitureCalculationTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _estateService.AddFurnitureCalculationType(furnitureCalculationType, LanguageIdTexts);
                    }
                }

            }
        }

        public void AddFurnitureGroupTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            FurnitureGroupType furnitureGroupType;
            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["FurnitureGroupType"];
                //Set the cell value using row and column.
                for (int numberOfFurnitureGroupTypes = 0; numberOfFurnitureGroupTypes < ws.Cells[1, 1].GetValue<int>(); numberOfFurnitureGroupTypes++)
                {
                    furnitureGroupType = new FurnitureGroupType()
                    {
                        Id = ws.Cells[4 + numberOfFurnitureGroupTypes, 2].GetValue<int>(),
                        LocalKey = "FurnitureGroupType.Id." + ws.Cells[4 + numberOfFurnitureGroupTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfFurnitureGroupTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfFurnitureGroupTypes, 4].GetValue<string>()
                    };

                    if (null == _estateService.GetFurnitureGroupType(furnitureGroupType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfFurnitureGroupTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfFurnitureGroupTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _estateService.AddFurnitureGroupType(furnitureGroupType, LanguageIdTexts);
                    }
                }

            }
        }
        public void AddFurnitureTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            FurnitureType furnitureType;
            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["FurnitureType"];
                //Set the cell value using row and column.
                for (int numberOfFurnitureTypes = 0; numberOfFurnitureTypes < ws.Cells[1, 1].GetValue<int>(); numberOfFurnitureTypes++)
                {
                    furnitureType = new FurnitureType()
                    {
                        Id = ws.Cells[4 + numberOfFurnitureTypes, 2].GetValue<int>(),
                        LocalKey = "FurnitureType.Id." + ws.Cells[4 + numberOfFurnitureTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfFurnitureTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfFurnitureTypes, 4].GetValue<string>(),
                        FurnitureCalculationTypeId = ws.Cells[4 + numberOfFurnitureTypes, 19].GetValue<int>(),
                        FurnitureGroupTypeId = ws.Cells[4 + numberOfFurnitureTypes, 1].GetValue<int>(),
                        Volume = ws.Cells[4 + numberOfFurnitureTypes, 17].GetValue<decimal>(),
                        IsActive = true,
                        Assemblable = ws.Cells[4 + numberOfFurnitureTypes, 20].GetValue<string>() == "x" ? true : false,

                    };
                    if (furnitureType.Assemblable)
                    {
                        furnitureType.AssembleCost = ws.Cells[4 + numberOfFurnitureTypes, 21].GetValue<decimal>();
                        furnitureType.DisassembleCost = ws.Cells[4 + numberOfFurnitureTypes, 23].GetValue<decimal>();
                        if (furnitureType.FurnitureCalculationTypeId == 3)
                        {
                            furnitureType.FlatRate = ws.Cells[4 + numberOfFurnitureTypes, 24].GetValue<decimal>();
                        }
                    }

                    if (null == _estateService.GetFurnitureType(furnitureType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfFurnitureTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfFurnitureTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _estateService.AddFurnitureType(furnitureType, LanguageIdTexts);
                    }
                }

            }
        }

        public void AddEPartTypeFrnGrpType()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");

            EPartTypeFrnGrpType ePartTypeFrnGrpType;
            int value;
            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["FurnitureGroupType"];
                var numberOfFurnitureGroupTypes = ws.Cells[1, 1].GetValue<int>();
                var numberOfEstatePartTypes = ws.Cells[1, 3].GetValue<int>();
                //Set the cell value using row and column.
                for (int row = 0; row < numberOfFurnitureGroupTypes; row++)
                {
                    for (int col = 0; col < numberOfEstatePartTypes; col++)
                    {
                        value = ws.Cells[row + 4, col + 17].GetValue<int>();
                        if (0 == value)
                            continue;

                        ePartTypeFrnGrpType = new EPartTypeFrnGrpType()
                        {
                            SequenceNumber = value,
                            EstatePartTypeId = col + 1,
                            FurnitureGroupTypeId = row + 1
                        };
                        if (null != _estateService.GetEPartTypeFrnGrpTypeByTypeId(ePartTypeFrnGrpType.EstatePartTypeId,
                            ePartTypeFrnGrpType.FurnitureGroupTypeId))
                        {
                            _estateService.UpdateEPartTypeFrnGrpType(ePartTypeFrnGrpType);
                        }
                        else
                        {
                            _estateService.AddEPartTypeFrnGrpType(ePartTypeFrnGrpType);
                        }
                    }
                }
            }
        }

        public void EstateTypeEPartType()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "Estate.xlsx");
            string value;
            EstateTypeEPartType estateTypeEPartType;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["EstateTypeEPartType"];
                var numberOfEstateTypes = ws.Cells[1, 1].GetValue<int>();
                var numberOfEstatePartTypes = ws.Cells[1, 2].GetValue<int>();
                int sequenceNum = 1;
                //Set the cell value using row and column.
                for (int row = 0; row < numberOfEstateTypes; row++)
                {
                    sequenceNum = 1;
                    for (int col = 0; col < numberOfEstatePartTypes; col++)
                    {
                        value = ws.Cells[row + 4, col + 3].GetValue<string>();
                        if (null == value)
                            continue;

                        if (value.ToLower().Equals("x"))
                        {
                            estateTypeEPartType = new EstateTypeEPartType()
                            {
                                SequenceNumber = sequenceNum++,
                                EstatePartTypeId = col + 1,
                                EstateTypeId = row + 1
                            };
                            if (null != _estateService.GetEstateTypeEPartTypeByTypeId(
                                estateTypeEPartType.EstateTypeId,
                                estateTypeEPartType.EstatePartTypeId))
                            {
                                _estateService.UpdateEstateTypeEPartType(estateTypeEPartType);
                            }
                            else
                            {
                                _estateService.AddEstateTypeEPartType(estateTypeEPartType);
                            }

                        }
                    }
                }
            }
        }

        #endregion Estate

        #region Demand
        public void AddDemandTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            Task.Delay(2000).Wait();
            var fi = new FileInfo(_dataDocumentPath + "A-Priv. Umzugsliste.xlsx");
            DemandType demandType;

            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["DemandType"];
                //Set the cell value using row and column.
                for (int numberOfDemandTypes = 0; numberOfDemandTypes < ws.Cells[1, 1].GetValue<int>(); numberOfDemandTypes++)
                {
                    demandType = new DemandType()
                    {
                        Id = ws.Cells[4 + numberOfDemandTypes, 2].GetValue<int>(),
                        LocalKey = "DemandType.Id." + ws.Cells[4 + numberOfDemandTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfDemandTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfDemandTypes, 4].GetValue<string>()
                    };

                    if (null == _demandService.GetDemandType(demandType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfDemandTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfDemandTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _demandService.AddDemandType(demandType, LanguageIdTexts);
                    }
                }

            }
        }

        public void AddDemandStatusTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "A-Priv. Umzugsliste.xlsx");
            DemandStatusType demandStatusType;

            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["DemandStatusType"];
                //Set the cell value using row and column.
                for (int numberOfDemandStatusTypes = 0; numberOfDemandStatusTypes < ws.Cells[1, 1].GetValue<int>(); numberOfDemandStatusTypes++)
                {
                    demandStatusType = new DemandStatusType()
                    {
                        Id = ws.Cells[4 + numberOfDemandStatusTypes, 2].GetValue<int>(),
                        LocalKey = "DemandStatusType.Id." + ws.Cells[4 + numberOfDemandStatusTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfDemandStatusTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfDemandStatusTypes, 4].GetValue<string>()
                    };

                    if (null == _demandService.GetDemandStatusType(demandStatusType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfDemandStatusTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfDemandStatusTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _demandService.AddDemandStatusType(demandStatusType, LanguageIdTexts);
                    }
                }

            }
        }
        #endregion Demand

        #region Address
        public void AddAdressTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "A-Priv. Umzugsliste.xlsx");
            GenericAddressType genericAddressType;

            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["GenericAddressType"];
                //Set the cell value using row and column.
                for (int numberOfGenericAddressTypes = 0; numberOfGenericAddressTypes < ws.Cells[1, 1].GetValue<int>(); numberOfGenericAddressTypes++)
                {
                    genericAddressType = new GenericAddressType()
                    {
                        Id = ws.Cells[4 + numberOfGenericAddressTypes, 2].GetValue<int>(),
                        LocalKey = "GenericAddressType.Id." + ws.Cells[4 + numberOfGenericAddressTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfGenericAddressTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfGenericAddressTypes, 4].GetValue<string>()
                    };

                    if (null == _genericAddressService.GetGenericAddressType(genericAddressType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfGenericAddressTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfGenericAddressTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _genericAddressService.AddGenericAddressType(genericAddressType, LanguageIdTexts);
                    }
                }

            }

        }
        #endregion Address

        public void AddCountries()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "A-Priv. Umzugsliste.xlsx");

            Country country;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["CountryList"];
                var numberOfCountries = ws.Cells[1, 1].GetValue<int>();
                var numberOfFields = ws.Cells[1, 2].GetValue<int>();
                var numberOfLanguages = ws.Cells[1, 3].GetValue<int>();

                List<LanguageIdText> languageIdTexts;
                LanguageIdText languageIdText;

                //Set the cell value using row and column.
                for (int countryIndex = 1; countryIndex <= numberOfCountries; countryIndex++)
                {
                    country = new Country()
                    {
                        Id = ws.Cells[2 + countryIndex, 1].GetValue<int>(),
                        Name = ws.Cells[2 + countryIndex, 2].GetValue<string>(),
                        CountryCode = ws.Cells[2 + countryIndex, 3].GetValue<string>(),
                        AreaCodes = ws.Cells[2 + countryIndex, 4].GetValue<string>(),
                        CountryCodeLength = ws.Cells[2 + countryIndex, 5].GetValue<int>(),
                        PhoneAreaCodeMinLength = ws.Cells[2 + countryIndex, 6].GetValue<int>(),
                        PhoneAreaCodeMaxLength = ws.Cells[2 + countryIndex, 7].GetValue<int>(),
                        PhoneSubscriberNumberLengthMin = ws.Cells[2 + countryIndex, 8].GetValue<int>(),
                        PhoneSubscriberNumberLengthMax = ws.Cells[2 + countryIndex, 9].GetValue<int>(),
                        IsoCode2 = ws.Cells[2 + countryIndex, 10].GetValue<string>(),
                        IsoCode3 = ws.Cells[2 + countryIndex, 11].GetValue<string>(),
                        IsActive = ws.Cells[2 + countryIndex, 12].GetValue<string>() == "true" ? true : false,
                        CultureName = ws.Cells[2 + countryIndex, 13].GetValue<string>(),
                        LocalKey = "Country.Id." + ws.Cells[2 + countryIndex, 1].GetValue<int>().ToString()

                    };

                    if (null == _countryService.Get(country.Id))
                    {
                        languageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 3].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[2 + countryIndex, 14 + i].GetValue<string>(),
                                Description = ws.Cells[2 + countryIndex, 14 + i].GetValue<string>()
                            };
                            languageIdTexts.Add(languageIdText);
                        }
                        _countryService.Add(country, languageIdTexts);
                    }
                }
            }
        }

        public void AddCompanyStatusTypes()
        {
            //Open the workbook (or create it if it doesn't exist)
            var fi = new FileInfo(_dataDocumentPath + "A-Priv. Umzugsliste.xlsx");
            CompanyStatusType companyStatusType;

            List<LanguageIdText> LanguageIdTexts;
            LanguageIdText languageIdText;

            using (var p = new ExcelPackage(fi))
            {
                //Get the Worksheet created in the previous codesample. 
                var ws = p.Workbook.Worksheets["CompanyStatusTypes"];
                //Set the cell value using row and column.
                for (int numberOfCompanyStatusTypes = 0; numberOfCompanyStatusTypes < ws.Cells[1, 1].GetValue<int>(); numberOfCompanyStatusTypes++)
                {
                    companyStatusType = new CompanyStatusType()
                    {
                        Id = ws.Cells[4 + numberOfCompanyStatusTypes, 2].GetValue<int>(),
                        LocalKey = "CompanyStatusType.Id." + ws.Cells[4 + numberOfCompanyStatusTypes, 2].GetValue<int>().ToString(),
                        Name = ws.Cells[4 + numberOfCompanyStatusTypes, 3].GetValue<string>(),
                        Description = ws.Cells[4 + numberOfCompanyStatusTypes, 4].GetValue<string>()
                    };

                    if (null == _companyService.GetCompanyStatusType(companyStatusType.Id))
                    {
                        LanguageIdTexts = new List<LanguageIdText>();
                        for (int i = 0; i < ws.Cells[1, 2].GetValue<int>(); i++)
                        {
                            languageIdText = new LanguageIdText()
                            {
                                LanguageId = i + 1,
                                CultureName = cultureNameList[i],
                                Text = ws.Cells[4 + numberOfCompanyStatusTypes, 5 + i * 2].GetValue<string>(),
                                Description = ws.Cells[4 + numberOfCompanyStatusTypes, 6 + i * 2].GetValue<string>(),
                            };
                            LanguageIdTexts.Add(languageIdText);
                        }
                        _companyService.AddCompanyStatusType(companyStatusType, LanguageIdTexts);
                    }
                }

            }
        }
    }

}
