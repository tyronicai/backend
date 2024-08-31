using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAK.Model.Localization;

namespace OAK.ModelConfiguration.LocalizationConfiguration
{
    public class LocalizationViewConfiguration : IEntityTypeConfiguration<LocalizationView>
    {
        public void Configure(EntityTypeBuilder<LocalizationView> builder)
        {
            builder.HasNoKey();
            builder.ToView("LocalizationView");
        }
    }
}