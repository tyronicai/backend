namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BaseModels;
    public class FurnitureCalculationTypeConfiguration : IEntityTypeConfiguration<FurnitureCalculationType>
    {
        public void Configure(EntityTypeBuilder<FurnitureCalculationType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.ConfigureLocalization();
            builder.ToTable("FurnitureCalculationTypes");
        }
    }
}
