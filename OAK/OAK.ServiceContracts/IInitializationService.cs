namespace OAK.ServiceContracts
{
    public interface IInitializationService
    {
        void AddAdminAccount();
        void AddDefaultLanguages();
        void AddEstateTypes();
        void AddFlatTypes();
        void AddEstatePartTypes();
        void AddFurnitureCalculationTypes();
        void AddFurnitureGroupTypes();
        void AddFurnitureTypes();
        void AddEPartTypeFrnGrpType();
        void EstateTypeEPartType();
        void AddDemandTypes();
        void AddDemandStatusTypes();
        void AddAdressTypes();
        void AddCountries();
        void AddCompanyStatusTypes();
        bool IsDatabasePopulated();
    }


}
