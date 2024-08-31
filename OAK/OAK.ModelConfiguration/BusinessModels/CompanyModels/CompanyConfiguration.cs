namespace OAK.ModelConfiguration.BusinessModels.DocumentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CompanyModels;

    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.CompanyStatusType).WithMany(x => x.Companies).HasForeignKey(x => x.CompanyStatusTypeId);

            builder.ToTable("Companies");
        }
    }
}
