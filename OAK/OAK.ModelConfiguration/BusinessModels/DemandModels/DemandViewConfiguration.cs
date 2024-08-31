using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAK.Model.BusinessModels.DemandModels;

namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    public class DemandViewConfiguration : IEntityTypeConfiguration<DemandView>
    {
        public void Configure(EntityTypeBuilder<DemandView> builder)
        {
            builder.HasNoKey();
            builder.ToView("DemandView");
        }
    }
}