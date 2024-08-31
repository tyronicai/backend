namespace OAK.ModelConfiguration.LocalizationConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Localization;
    public class LocalizationTextConfiguration : IEntityTypeConfiguration<LocalizationText>
    {
        public void Configure(EntityTypeBuilder<LocalizationText> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.LanguageId).IsRequired();
            builder.HasOne(x => x.LocalizationKey).WithMany(x => x.LocalizationTexts).HasForeignKey(x => x.LocalKey);
            builder.HasOne(x => x.Language).WithMany(x => x.LocalizationTexts).HasForeignKey(x => x.LanguageId);

            builder.HasAlternateKey(x => new { x.LanguageId, x.LocalKey });

            builder.ToTable("LocalizationText");
        }
    }
}
