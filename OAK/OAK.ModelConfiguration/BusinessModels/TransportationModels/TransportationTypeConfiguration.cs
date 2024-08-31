namespace OAK.ModelConfiguration.BusinessModels.TransportationModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.TransportationModels;
    public class TransportationTypeConfiguration : IEntityTypeConfiguration<TransportationType>
    {
        public void Configure(EntityTypeBuilder<TransportationType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(p => p.PropertyJson).WithMany(p => p.TransportationTypes).HasForeignKey(p => p.PropertyJsonId);
            builder.ToTable("TransportationTypes");
        }
    }
}
