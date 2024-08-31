namespace OAK.ModelConfiguration.LocalizationConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Localization;
    public class LocalizationKeyConfiguration : IEntityTypeConfiguration<LocalizationKey>
    {
        public void Configure(EntityTypeBuilder<LocalizationKey> builder)
        {
            builder.HasKey(f => f.Key);
            builder.Property(f => f.Name).IsRequired().HasMaxLength(255);
            builder.ToTable("LocalizationKey");
        }
    }
}
