namespace OAK.ModelConfiguration.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;
    public class PropertyConfiguration : IEntityTypeConfiguration<PropertyJson>
    {
        public void Configure(EntityTypeBuilder<PropertyJson> builder)
        {

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(255);
            builder.Property(p => p.JsonObject).IsRequired().HasMaxLength(1000);


            builder.ToTable("PropertyJson");
        }
    }
}