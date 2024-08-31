namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.DemandModels;

    public class DemandConfiguration : IEntityTypeConfiguration<Demand>
    {
        public void Configure(EntityTypeBuilder<Demand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Transportations);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasOne(a => a.Account).WithMany(m => m.Demands).HasForeignKey(fk => fk.AccountId);
            builder.HasOne(dow => dow.DemandOwner).WithOne(d => d.Demand).HasForeignKey<Demand>(d => d.DemandOwnerId);


            //builder.HasOne(aoi => aoi.CompanyDemandService).WithOne(d => d.Demand)
            //  .HasForeignKey<Demand>(d => d.AcceptedOfferId);

            builder.HasMany(d => d.CompanyDemandServices).WithOne(cds => cds.Demand).HasForeignKey(fk => fk.DemandId);


            builder.ToTable("Demands");
        }
    }
}
