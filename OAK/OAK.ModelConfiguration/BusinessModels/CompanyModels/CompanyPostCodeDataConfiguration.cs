namespace OAK.ModelConfiguration.Core
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;
    using OAK.ModelConfiguration.BaseModels;
    public class CompanyPostCodeDataConfiguration : IEntityTypeConfiguration<CompanyPostCodeData>
    {
        public void Configure(EntityTypeBuilder<CompanyPostCodeData> builder)
        {
            builder.HasKey(k => k.Id);
            builder.ConfigureBase();
            builder.HasOne(x => x.Company).WithMany(x => x.CompanyPostCodeDatas).HasForeignKey(x => x.CompanyId);
            builder.HasOne(x => x.Country).WithMany(x => x.CompanyPostCodeDatas).HasForeignKey(x => x.CountryId);
            builder.ToTable("CompanyPostCodeData");
        }
    }
}
