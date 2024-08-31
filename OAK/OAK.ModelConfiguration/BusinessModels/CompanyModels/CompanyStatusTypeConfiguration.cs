namespace OAK.ModelConfiguration.BusinessModels.CommentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CompanyModels;
    using OAK.ModelConfiguration.BaseModels;

    public class CompanyStatusTypeConfiguration : IEntityTypeConfiguration<CompanyStatusType>
    {
        public void Configure(EntityTypeBuilder<CompanyStatusType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ConfigureLocalization();
            builder.ToTable("CompanyStatusTypes");
        }
    }
}
