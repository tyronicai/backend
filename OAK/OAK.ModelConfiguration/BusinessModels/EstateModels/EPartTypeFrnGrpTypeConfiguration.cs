namespace OAK.ModelConfiguration.BusinessModels.EstateModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.EstateModels;

    public class EPartTypeFrnGrpTypeConfiguration : IEntityTypeConfiguration<EPartTypeFrnGrpType>
    {
        public void Configure(EntityTypeBuilder<EPartTypeFrnGrpType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("EPartTypeFrnGrpTypes");
        }
    }
}
