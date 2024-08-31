namespace OAK.ModelConfiguration.BaseModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BaseModels;
    public static class EntityBaseConfiguration
    {
        public static void ConfigureBase<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : ModelBase
        {
            builder.Property(e => e.Created).IsRequired().HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.Modified).HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}