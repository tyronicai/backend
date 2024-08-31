using OAK.Model.Core;

namespace OAK.ServiceContracts
{
    using OAK.Data;
    using OAK.Data.Paging;

    public interface IPropertyJsonService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        IPaginate<PropertyJson> GetAllPropertyJsons(int index, int size);
        bool AddPropertyJson(PropertyJson propertyJson);
        bool UpdatePropertyJson(PropertyJson propertyJson);
        PropertyJson GetPropertyJson(int id);
        bool DeletePropertyJson(int Id);

    }

}
