namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BaseModels;

    public class EstateTypeConfiguration : IEntityTypeConfiguration<EstateType>
    {
        public void Configure(EntityTypeBuilder<EstateType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.HasOne(x => x.PropertyJson).WithMany(x => x.EstateTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.ConfigureLocalization();
            builder.ToTable("EstateTypes");
        }
    }
}
