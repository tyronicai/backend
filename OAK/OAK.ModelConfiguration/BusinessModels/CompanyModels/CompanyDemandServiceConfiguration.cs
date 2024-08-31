namespace OAK.ModelConfiguration.BusinessModels.DocumentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CompanyModels;

    public class CompanyDemandServiceConfiguration : IEntityTypeConfiguration<CompanyDemandService>
    {
        public void Configure(EntityTypeBuilder<CompanyDemandService> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            // builder.HasMany(d => d.Demand).HasForeignKey<CompanyDemandService>(cds => cds.DemandId);
            builder.ToTable("CompanyDemandServices");
        }
    }
}
