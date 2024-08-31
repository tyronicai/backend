using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAK.Model.BusinessModels.EstateModels;

namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    public class EstateDetailViewConfiguration : IEntityTypeConfiguration<EstateDetailView>
    {
        public void Configure(EntityTypeBuilder<EstateDetailView> builder)
        {
            builder.HasNoKey();
            builder.ToView("EstateDetailView");
        }
    }
}