namespace OAK.ModelConfiguration.BusinessModels.CommentModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.CommentModels;

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Account).WithMany(x => x.Comments).HasForeignKey(x => x.AccountId);
            builder.ToTable("Comments");
        }
    }
}
