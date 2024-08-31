namespace OAK.ModelConfiguration.BusinessModels.CommentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CommentModels;
    using OAK.ModelConfiguration.BaseModels;

    public class CommentStatusTypeConfiguration : IEntityTypeConfiguration<CommentStatusType>
    {
        public void Configure(EntityTypeBuilder<CommentStatusType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ConfigureLocalization();
            builder.ToTable("CommentStatusTypes");
        }
    }
}
