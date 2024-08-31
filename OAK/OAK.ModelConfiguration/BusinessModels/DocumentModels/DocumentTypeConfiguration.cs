namespace OAK.ModelConfiguration.BusinessModels.DocumentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.DocumentModels;
    using OAK.ModelConfiguration.BaseModels;

    public class DocumentTypeConfiguration : IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.PropertyJson).WithMany(x => x.DocumentTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.ConfigureLocalization();
            builder.ToTable("DocumentTypes");
        }
    }
}
