namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;

    public class EstatePartFurnituresConfiguration : IEntityTypeConfiguration<EstatePartFurniture>
    {
        public void Configure(EntityTypeBuilder<EstatePartFurniture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("EstatePartFurnitures");
        }
    }
}
