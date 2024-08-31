namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BaseModels;
    public class FurnitureGroupTypeConfiguration : IEntityTypeConfiguration<FurnitureGroupType>
    {
        public void Configure(EntityTypeBuilder<FurnitureGroupType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.ConfigureLocalization();
            builder.ToTable("FurnitureGroupTypes");
        }
    }
}
