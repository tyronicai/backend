using OAK.Data;
using OAK.Data.Paging;
using OAK.Model.BusinessModels.ParameterModels;
using OAK.ServiceContracts;

namespace OAK.Services
{
    public class ParameterService : IParameterService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public ParameterService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        #region Parameters
        public IPaginate<Parameters> GetAllParameters(int index, int size)
        {
            IPaginate<Parameters> items = UnitOfWork.GetReadOnlyRepository<Parameters>().GetList(index: index, size: size);
            return items;
        }

        public bool AddParameters(Parameters parameters)
        {

            UnitOfWork.GetRepository<Parameters>().Add(parameters);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateParameters(Parameters parameters)
        {
            Parameters oldRecord = UnitOfWork.GetRepository<Parameters>().Single(x => x.Id == parameters.Id);


            //map
            //oldRecord.TransportationTypeId = parameters.TransportationTypeId;

            UnitOfWork.GetRepository<Parameters>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }

        public Parameters GetParameters(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<Parameters>().Single(predicate: x => x.Id == id);
        }

        public bool DeleteParameters(int Id)
        {
            Parameters record = UnitOfWork.GetRepository<Parameters>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<Parameters>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion Parameters

        #region CurrencyParameters
        public IPaginate<CurrencyParameters> GetAllCurrencyParameters(int index, int size)
        {
            IPaginate<CurrencyParameters> items = UnitOfWork.GetReadOnlyRepository<CurrencyParameters>().GetList(index: index, size: size);
            return items;
        }

        public bool AddCurrencyParameters(CurrencyParameters currencyParameters)
        {
            LocalizationService.ControlAndAdd(currencyParameters.LocalKey);

            UnitOfWork.GetRepository<CurrencyParameters>().Add(currencyParameters);
            int affectedRows = UnitOfWork.SaveChanges();

            return affectedRows > 0;

        }

        public bool UpdateCurrencyParameters(CurrencyParameters currencyParameters)
        {
            CurrencyParameters oldRecord = UnitOfWork.GetRepository<CurrencyParameters>().Single(x => x.Id == currencyParameters.Id);


            //map
            //oldRecord.TransportationTypeId = estate.TransportationTypeId;

            UnitOfWork.GetRepository<CurrencyParameters>().Update(oldRecord);
            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;

        }
        public CurrencyParameters GetCurrencyParameters(int id)
        {
            return UnitOfWork.GetReadOnlyRepository<CurrencyParameters>().Single(predicate: x => x.Id == id);

        }
        public bool DeleteCurrencyParameters(int Id)
        {
            CurrencyParameters record = UnitOfWork.GetRepository<CurrencyParameters>().Single(x => x.Id == Id);

            if (null == record)
            {
                return false;
            }

            //should be removed
            //LocalizationService.ControlAndAdd(country.LocalKey);

            UnitOfWork.GetRepository<CurrencyParameters>().Delete(record);

            int affectedRows = UnitOfWork.SaveChanges();
            return affectedRows > 0;
        }
        #endregion CurrencyParameters

    }
}
