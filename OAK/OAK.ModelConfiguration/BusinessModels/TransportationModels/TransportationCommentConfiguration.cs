namespace OAK.ModelConfiguration.BusinessModels.TransportationModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.TransportationModels;
    public class TransportationCommentConfiguration : IEntityTypeConfiguration<TransportationComment>
    {
        public void Configure(EntityTypeBuilder<TransportationComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.ToTable("TransportationComments");
        }
    }
}
