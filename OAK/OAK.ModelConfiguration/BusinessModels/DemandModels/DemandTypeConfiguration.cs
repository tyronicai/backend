namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.DemandModels;
    using OAK.ModelConfiguration.BaseModels;

    public class DemandTypeConfiguration : IEntityTypeConfiguration<DemandType>
    {
        public void Configure(EntityTypeBuilder<DemandType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.PropertyJson).WithMany(x => x.DemandTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.ConfigureLocalization();
            builder.ToTable("DemandTypes");
        }
    }
}
