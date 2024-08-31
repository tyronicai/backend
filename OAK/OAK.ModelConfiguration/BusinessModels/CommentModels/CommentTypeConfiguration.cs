namespace OAK.ModelConfiguration.BusinessModels.CommentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CommentModels;
    using OAK.ModelConfiguration.BaseModels;

    public class CommentTypeConfiguration : IEntityTypeConfiguration<CommentType>
    {
        public void Configure(EntityTypeBuilder<CommentType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.PropertyJson).WithMany(x => x.CommentTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.ConfigureLocalization();
            builder.ToTable("CommentTypes");
        }
    }
}
