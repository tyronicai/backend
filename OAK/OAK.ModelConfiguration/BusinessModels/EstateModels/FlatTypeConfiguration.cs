namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;
    using OAK.ModelConfiguration.BaseModels;

    public class FlatTypeConfiguration : IEntityTypeConfiguration<FlatType>
    {
        public void Configure(EntityTypeBuilder<FlatType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ConfigureLocalization();
            builder.ToTable("FlatTypes");
        }
    }
}
