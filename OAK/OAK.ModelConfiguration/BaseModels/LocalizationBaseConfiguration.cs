namespace OAK.ModelConfiguration.BaseModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BaseModels;
    public static class LocalizationBaseConfiguration
    {
        public static void ConfigureLocalization<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : LocalizationModelBase
        {
            builder.HasOne(x => x.LocalizationKey).WithMany().HasForeignKey(x => x.LocalKey).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
