namespace OAK.ModelConfiguration.BusinessModels.TransportationModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.TransportationModels;
    public class TransportationStatusTypeConfiguration : IEntityTypeConfiguration<TransportationStatusType>
    {
        public void Configure(EntityTypeBuilder<TransportationStatusType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("TransportationStatusTypes");
        }
    }
}
