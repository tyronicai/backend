namespace OAK.ModelConfiguration.BusinessModels.DocumentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CompanyModels;

    public class CompanyOfficialDocumentConfiguration : IEntityTypeConfiguration<CompanyOfficialDocument>
    {
        public void Configure(EntityTypeBuilder<CompanyOfficialDocument> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("CompanyOfficialDocuments");
        }
    }
}
