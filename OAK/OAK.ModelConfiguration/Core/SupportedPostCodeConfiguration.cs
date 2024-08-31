namespace OAK.ModelConfiguration.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;

    public class SupportedPostCodeConfiguration : IEntityTypeConfiguration<SupportedPostCode>
    {
        public void Configure(EntityTypeBuilder<SupportedPostCode> builder)
        {
            builder.HasKey(k => k.Id);
            builder.HasOne(c => c.Country).WithMany(m => m.SupportedPostCodes).HasForeignKey(k => k.CountryId);
            builder.ToTable("SupportedPostCodes");

        }
    }
}
