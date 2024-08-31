using OAK.Data;
using OAK.Data.Paging;
using OAK.Model.Core;
using OAK.ServiceContracts;

namespace OAK.Services
{
    public class PropertyJsonService : IPropertyJsonService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public PropertyJsonService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        #region PropertyJson
        public IPaginate<PropertyJson> GetAllPropertyJsons(int index, int size)
        {
            IPaginate<PropertyJson> items = UnitOfWork.GetReadOnlyRepository<PropertyJson>().GetList(index: index, size: size);
            return items;
        }

        public bool AddPropertyJson(PropertyJson propertyJson)
        {

            UnitOfWork.GetRepository<PropertyJson>().Add(propertyJson);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdatePropertyJson(PropertyJson propertyJson)
        {
            PropertyJson oldRecord = UnitOfWork.GetRepository<PropertyJson>().Single(x => x.Id == propertyJson.Id);


            //map
            //oldRecord.PropertyJsonTypeId = propertyJson.PropertyJsonTypeId;

            UnitOfWork.GetRepository<PropertyJson>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public PropertyJson GetPropertyJson(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<PropertyJson>().Single(predicate: x => x.Id == id);
        }

        public bool DeletePropertyJson(int Id)
        {
            PropertyJson record = UnitOfWork.GetRepository<PropertyJson>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<PropertyJson>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion PropertyJson

    }
}
