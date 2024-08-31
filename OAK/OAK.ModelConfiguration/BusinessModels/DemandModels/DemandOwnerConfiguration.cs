namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.DemandModels;

    public class DemandOwnerConfiguration : IEntityTypeConfiguration<DemandOwner>
    {
        public void Configure(EntityTypeBuilder<DemandOwner> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(o => o.Demand).WithOne(w => w.DemandOwner).HasForeignKey<DemandOwner>(fk => fk.DemandId).IsRequired();
            builder.ToTable("DemandOwners");
        }
    }
}
