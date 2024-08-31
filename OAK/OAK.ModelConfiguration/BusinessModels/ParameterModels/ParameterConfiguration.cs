namespace OAK.ModelConfiguration.BusinessModels.ParameterModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.ParameterModels;
    public class ParametersConfiguration : IEntityTypeConfiguration<Parameters>
    {
        public void Configure(EntityTypeBuilder<Parameters> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Parameters");
        }
    }
}
