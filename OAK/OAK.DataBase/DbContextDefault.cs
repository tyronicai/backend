using OAK.Model.BusinessModels.CompanyModels;

namespace OAK.DataBase
{
    using Microsoft.EntityFrameworkCore;
    using OAK.Model.BusinessModels;
    using OAK.Model.BusinessModels.AddressModels;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.Model.BusinessModels.DocumentModels;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.Model.BusinessModels.TransportationModels;
    using OAK.Model.Core;
    using OAK.ModelConfiguration.Core;    
    using OAK.Model.Localization;
    using OAK.ModelConfiguration.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BusinessModels.TransportationModels;
    using OAK.ModelConfiguration.BusinessModels.ParameterModels;
    using OAK.ModelConfiguration.LocalizationConfiguration;
    using OAK.ModelConfiguration.BusinessModels.DocumentModels;
    using OAK.ModelConfiguration.BusinessModels.CompanyModels;
    using OAK.ModelConfiguration.BusinessModels.CommentModels;
    using OAK.ModelConfiguration.BusinessModels;
    using OAK.ModelConfiguration.BusinessModels.AddressModels;
    using OAK.ModelConfiguration.BusinessModels.DemandModels;

    public class DbContextDefault : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<LocalizationKey> LocalizationKeys { get; set; }
        public DbSet<LocalizationText> LocalizationTexts { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<PostCodeData> PostCodes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<SupportedPostCode> SupportedPostCodes { get; set; }

        public DbSet<GenericAddress> CustomerAddresses { get; set; }
        public DbSet<GenericAddressType> CustomerAddressTypes { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyStatusType> CompanyStatusTypes { get; set; }
        public DbSet<CompanyDemandView> CompanyDemandViews { get; set; }
        public DbSet<CompanyPostCodeData> CompanyPostCodeData { get; set; }

        #region DemandModels

        public DbSet<DemandType> DemandTypes { get; set; }
        public DbSet<DemandStatusType> DemandStatusTypes { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<DemandOwner> DemandOwners { get; set; }
        public DbSet<DemandComment> DemandComments { get; set; }
        public DbSet<DemandChat> DemandChats { get; set; }
        public DbSet<DemandView> DemandViews { get; set; }

        #endregion DemandModels

        #region DocumentModels

        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Document> Documents { get; set; }

        #endregion DocumentModels

        #region EstateModels

        public DbSet<EstateType> EstateTypes { get; set; }
        public DbSet<Estate> Estates { get; set; }
        public DbSet<FlatType> FlatTypes { get; set; }
        public DbSet<EstatesFlat> EstatesFlats { get; set; }
        public DbSet<EstatePartType> EstatePartTypes { get; set; }
        public DbSet<EstatePart> EstateParts { get; set; }
        public DbSet<FurnitureGroupType> FurnitureGroupTypes { get; set; }
        public DbSet<FurnitureCalculationType> FurnitureCalculationTypes { get; set; }
        public DbSet<FurnitureType> FurnitureTypes { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<EstatePartFurniture> EstatePartFurnitures { get; set; }
        public DbSet<EstateTypeEPartType> EstateTypeEPartTypeMatrices { get; set; }
        public DbSet<EPartTypeFrnGrpType> EPartTypeFrnGrpTypeMatrices { get; set; }
        public DbSet<EstateDetailView> EstateDetailViews { get; set; }

        #endregion EstateModels

        #region TransportModels

        public DbSet<Transportation> Transportations { get; set; }
        public DbSet<TransportationComment> TransportationComments { get; set; }
        public DbSet<TransportationDocument> TransportationDocuments { get; set; }
        public DbSet<TransportationStatusType> TransportationStatusTypes { get; set; }
        public DbSet<TransportationType> TransportationTypes { get; set; }

        #endregion TransportModels

        public DbSet<LocalizationView> LocalizationViews { get; set; }

        //public DbQuery<ViewLocalization> ViewLocalizations { get; set; }

        public DbContextDefault(DbContextOptions<DbContextDefault> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AccountRoleConfiguration());

            modelBuilder.ApplyConfiguration(new LocalizationKeyConfiguration());
            modelBuilder.ApplyConfiguration(new LocalizationTextConfiguration());

            modelBuilder.ApplyConfiguration(new PostCodeDataConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new SupportedPostCodeConfiguration());
            modelBuilder.ApplyConfiguration(new GenericAddressConfiguration());
            modelBuilder.ApplyConfiguration(new GenericAddressTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());

            #region CommentModels

            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CommentStatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CommentTypeConfiguration());

            #endregion CommentModels

            #region CompanyModels

            modelBuilder.ApplyConfiguration(new CompanyStatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyOfficialDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyPublicDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyDemandServiceConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyDemandViewConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyPostCodeDataConfiguration());

            #endregion CompanyModels

            #region DemandModels

            modelBuilder.ApplyConfiguration(new DemandTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DemandStatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DemandConfiguration());
            modelBuilder.ApplyConfiguration(new DemandOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new DemandCommentConfiguration());
            modelBuilder.ApplyConfiguration(new DemandChatConfiguration());
            modelBuilder.ApplyConfiguration(new DemandViewConfiguration());

            #endregion DemandModels

            #region DocumentModels

            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());

            #endregion DocumentModels

            #region EstateModels

            modelBuilder.ApplyConfiguration(new EstateTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EstateConfiguration());
            modelBuilder.ApplyConfiguration(new FlatTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EstatesFlatConfiguration());
            modelBuilder.ApplyConfiguration(new EstatePartTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EstatePartConfiguration());
            modelBuilder.ApplyConfiguration(new FurnitureGroupTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FurnitureCalculationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FurnitureTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FurnitureConfiguration());
            modelBuilder.ApplyConfiguration(new EstatePartFurnituresConfiguration());
            modelBuilder.ApplyConfiguration(new EstateTypeEPartTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EPartTypeFrnGrpTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EstateDetailViewConfiguration());

            #endregion EstateModels

            #region TransportModels

            modelBuilder.ApplyConfiguration(new TransportationConfiguration());
            modelBuilder.ApplyConfiguration(new TransportationCommentConfiguration());
            modelBuilder.ApplyConfiguration(new TransportationDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new TransportationStatusTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransportationTypeConfiguration());

            #endregion TransportModels

            #region ParameterModels

            modelBuilder.ApplyConfiguration(new ParametersConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyParametersConfiguration());

            #endregion ParameterModels

            //modelBuilder.Query<ViewLocalization>().ToView("View_Localization");            
            modelBuilder.ApplyConfiguration(new LocalizationViewConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}