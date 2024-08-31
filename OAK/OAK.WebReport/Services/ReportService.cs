using Microsoft.EntityFrameworkCore;
using OAK.Model.BusinessModels.AddressModels;
using OAK.Model.BusinessModels.DemandModels;
using OAK.Model.BusinessModels.EstateModels;
using OAK.Model.Core;
using OAK.Model.Localization;
using OAK.ServiceContracts;
using System;
using IReportService = OAK.WebReport.Services.ServiceContracts.IReportService;

namespace OAK.WebReport.Services
{
    using OAK.Data;
    using OAK.Data.Paging;
    public class ReportService : IReportService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IEmailService EmailService { get; }

        public ReportService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IPaginate<EstateDetailView> GetEstateDetailsAndFurnitures(int id)
        {
            try
            {
                var report = UnitOfWork.GetRepository<EstateDetailView>().GetList(x => x.Id == id);
                return report;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Account GetAccount(int accountId)
        {
            try
            {
                var account = UnitOfWork.GetRepository<Account>().Single(x => x.Id == accountId);
                return account;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DemandView GetDemandDetails(int demandId)
        {
            try
            {
                var demandView = UnitOfWork.GetRepository<DemandView>().Single(x => x.id == demandId);
                return demandView;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public GenericAddress GetEstateAddress(int addressId)
        {
            try
            {
                var genericAddress = UnitOfWork.GetRepository<GenericAddress>().Single(
                    x => x.Id == addressId,
                    null,
                    include: source => source
                        .Include(x => x.Country));
                return genericAddress;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetFurnitureName(string localKey, string cultureName)
        {
            if (cultureName == "de") cultureName = "de-DE";
            if (cultureName == "en") cultureName = "en-US";

            var furniture = UnitOfWork.GetRepository<LocalizationView>()
                .Single(x => x.LocalKey == localKey && x.CultureName == cultureName);
            return furniture.Text;
        }
    }
}