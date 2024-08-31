namespace OAK.ModelConfiguration.BusinessModels.DocumentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CompanyModels;

    public class CompanyPublicDocumentConfiguration : IEntityTypeConfiguration<CompanyPublicDocument>
    {
        public void Configure(EntityTypeBuilder<CompanyPublicDocument> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("CompanyPublicDocuments");
        }
    }
}
