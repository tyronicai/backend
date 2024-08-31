namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    public class FurnitureConfiguration : IEntityTypeConfiguration<Furniture>
    {
        public void Configure(EntityTypeBuilder<Furniture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("Furnitures");
        }
    }
}
