namespace OAK.ModelConfiguration.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;
    using OAK.ModelConfiguration.BaseModels;

    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.HasAlternateKey(x => new { x.CultureName });

            builder.ConfigureLocalization();
            builder.ToTable("Languages");
        }
    }
}
