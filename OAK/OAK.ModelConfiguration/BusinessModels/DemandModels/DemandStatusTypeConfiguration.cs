namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.ModelConfiguration.BaseModels;

    public class DemandStatusTypeConfiguration : IEntityTypeConfiguration<DemandStatusType>
    {
        public void Configure(EntityTypeBuilder<DemandStatusType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ConfigureLocalization();
            builder.ToTable("DemandStatusTypes");
        }
    }
}
