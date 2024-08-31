using Microsoft.EntityFrameworkCore.Storage;

namespace OAK.ServiceContracts
{
    using OAK.Model.Core;
    using OAK.Data;
    using OAK.Data.Paging;

    public interface IRoleService
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService LocalizationService { get; }
        void Add(Role role, IDbContextTransaction trans = null);
        void Update(Role role);
        Role GetDefault(bool createDefaultRoleIfNotExists, IDbContextTransaction trans = null);
        Role Get(int id);
    }

}