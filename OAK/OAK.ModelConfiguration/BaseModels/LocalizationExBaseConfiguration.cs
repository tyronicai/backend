namespace OAK.ModelConfiguration.BaseModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BaseModels;
    public static class LocalizationExBaseConfiguration
    {
        public static void ConfigureExLocalization<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : LocalizationExModelBase
        {
            builder.Property(x => x.CreateDate).IsRequired().HasColumnType("timestamp");
            builder.HasOne(x => x.LocalizationKey).WithMany().HasForeignKey(x => x.LocalKey).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
