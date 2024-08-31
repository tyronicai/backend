namespace OAK.ModelConfiguration.BusinessModels.TransportationModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.TransportationModels;
    public class TransportationDocumentConfiguration : IEntityTypeConfiguration<TransportationDocument>
    {
        public void Configure(EntityTypeBuilder<TransportationDocument> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("TransportationDocuments");
        }
    }
}
