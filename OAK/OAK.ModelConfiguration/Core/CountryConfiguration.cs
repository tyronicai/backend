namespace OAK.ModelConfiguration.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;
    using OAK.ModelConfiguration.BaseModels;

    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ConfigureLocalization();
            builder.HasKey(k => k.Id);
            builder.HasAlternateKey(k => k.IsoCode2);
            builder.ToTable("Country");
        }
    }
}
