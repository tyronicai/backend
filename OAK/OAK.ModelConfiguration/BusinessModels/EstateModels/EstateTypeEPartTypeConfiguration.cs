namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;

    public class EstateTypeEPartTypeConfiguration : IEntityTypeConfiguration<EstateTypeEPartType>
    {
        public void Configure(EntityTypeBuilder<EstateTypeEPartType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("EstateTypeEPartTypes");
        }
    }
}
