using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAK.Model.BusinessModels.CompanyModels;

namespace OAK.ModelConfiguration.BusinessModels.CompanyModels
{


    public class CompanyDemandViewConfiguration : IEntityTypeConfiguration<CompanyDemandView>
    {
        public void Configure(EntityTypeBuilder<CompanyDemandView> builder)
        {
            builder.HasNoKey();
            builder.ToView("CompanyDemandView");
        }
    }
}