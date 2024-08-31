namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.DemandModels;

    public class DemandCommentConfiguration : IEntityTypeConfiguration<DemandComment>
    {
        public void Configure(EntityTypeBuilder<DemandComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("DemandComments");
        }
    }
}
