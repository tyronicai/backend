namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BaseModels;

    public class EstatePartTypeConfiguration : IEntityTypeConfiguration<EstatePartType>
    {
        public void Configure(EntityTypeBuilder<EstatePartType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.HasOne(x => x.PropertyJson).WithMany(x => x.EstatePartTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.ConfigureLocalization();
            builder.ToTable("EstatePartTypes");
        }
    }
}
