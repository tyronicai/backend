namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;

    public class EstatesFlatConfiguration : IEntityTypeConfiguration<EstatesFlat>
    {
        public void Configure(EntityTypeBuilder<EstatesFlat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasOne(o => o.Estate).WithMany(m => m.Flats).HasForeignKey(fk => fk.EstateId);
            builder.ToTable("EstatesFlat");
        }
    }
}
