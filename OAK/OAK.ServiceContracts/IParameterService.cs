using OAK.Model.BusinessModels.ParameterModels;
using OAK.Data;
using OAK.Data.Paging;

namespace OAK.ServiceContracts
{
    public interface IParameterService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }

        IPaginate<Parameters> GetAllParameters(int index, int size);
        bool AddParameters(Parameters parameters);
        bool UpdateParameters(Parameters parameters);
        Parameters GetParameters(int id);
        bool DeleteParameters(int id);

        IPaginate<CurrencyParameters> GetAllCurrencyParameters(int index, int size);
        bool AddCurrencyParameters(CurrencyParameters currencyParameters);
        bool UpdateCurrencyParameters(CurrencyParameters currencyParameters);
        CurrencyParameters GetCurrencyParameters(int id);
        bool DeleteCurrencyParameters(int id);

    }

}
