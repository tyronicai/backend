namespace OAK.ModelConfiguration.BusinessModels.ParameterModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.ParameterModels;
    public class CurrencyParametersConfiguration : IEntityTypeConfiguration<CurrencyParameters>
    {
        public void Configure(EntityTypeBuilder<CurrencyParameters> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("CurrencyParameters");
        }
    }
}
