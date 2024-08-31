namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;

    public class EstatePartConfiguration : IEntityTypeConfiguration<EstatePart>
    {
        public void Configure(EntityTypeBuilder<EstatePart> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasOne(m => m.EstatesFlat).WithMany(m => m.EstateParts).HasForeignKey(f => f.EstatesFlatId);

            builder.ToTable("EstateParts");
        }
    }
}
