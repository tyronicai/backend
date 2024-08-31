using Microsoft.EntityFrameworkCore.Storage;

namespace OAK.Services
{
    using OAK.Data;
    using OAK.Model.Core;
    using OAK.ServiceContracts;
    using System;

    public class RoleService : IRoleService
    {
        public IUnitOfWork UnitOfWork { get; }
        public ILocalizationService LocalizationService { get; }

        public RoleService(IUnitOfWork unitOfWork, ILocalizationService localizationService)
        {
            UnitOfWork = unitOfWork;
            LocalizationService = localizationService;
        }

        public void Add(Role role, IDbContextTransaction trans = null)
        {
            role.CreateDate = DateTime.Now;
            bool localTrans = trans == null;
            trans = UnitOfWork.BeginTransaction(trans);

            LocalizationService.ControlAndAdd(role.LocalKey, null, trans);

            UnitOfWork.GetRepository<Role>().Add(role);
            UnitOfWork.SaveChanges();
            if (localTrans)
            {
                UnitOfWork.CommitTransaction(trans);
            }
        }

        public void Update(Role role)
        {
            Role oldRecord = UnitOfWork.GetRepository<Role>().Single(x => x.Id == role.Id);

            oldRecord.Name = role.Name;
            oldRecord.IsDefault = role.IsDefault;
            oldRecord.Description = role.Description;

            UnitOfWork.GetRepository<Role>().Update(oldRecord);
            UnitOfWork.SaveChanges();
        }

        public Role Get(int id)
        {
            return UnitOfWork.GetRepository<Role>().Single(predicate: x => x.Id == id);
        }

        const string DefaultRoleName = "DefaultRole";
        const string DefaultRoleDescription = "Default Role (Auto Added)";

        public Role GetDefault(bool createDefaultRoleIfNotExists, IDbContextTransaction trans)
        {
            bool localTrans = null == trans;
            trans = UnitOfWork.BeginTransaction(trans);

            var repo = UnitOfWork.GetRepository<Role>();
            Role defaultRole = repo.Single(predicate: x => x.IsDefault == true);

            if (defaultRole == null && createDefaultRoleIfNotExists)
            {
                var localizationKey = LocalizationService.ControlAndAdd(DefaultRoleName, null, trans);

                defaultRole = new Role()
                {
                    CreateDate = DateTime.Now,
                    Description = DefaultRoleDescription,
                    IsDefault = true,
                    Name = DefaultRoleName,
                    LocalKey = localizationKey.Key
                };

                repo.Add(defaultRole);
                UnitOfWork.SaveChanges();
            }
            if (localTrans)
                UnitOfWork.CommitTransaction(trans);

            return defaultRole;
        }
    }
}
