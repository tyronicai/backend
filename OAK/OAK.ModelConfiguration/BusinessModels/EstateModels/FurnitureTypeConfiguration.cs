namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BaseModels;
    public class FurnitureTypeConfiguration : IEntityTypeConfiguration<FurnitureType>
    {
        public void Configure(EntityTypeBuilder<FurnitureType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.PropertyJson).WithMany(x => x.FurnitureTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.HasOne(x => x.FurnitureCalculationType).WithMany(x => x.FurnitureTypes).HasForeignKey(x => x.FurnitureCalculationTypeId);
            builder.HasOne(x => x.FurnitureGroupType).WithMany(x => x.FurnitureTypes).HasForeignKey(x => x.FurnitureGroupTypeId);

            builder.ConfigureLocalization();
            builder.ToTable("FurnitureTypes");
        }
    }
}
