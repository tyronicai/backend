namespace OAK.ModelConfiguration.BusinessModels.AddressModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.AddressModels;
    using OAK.ModelConfiguration.BaseModels;

    public class GenericAddressTypeConfiguration : IEntityTypeConfiguration<GenericAddressType>
    {
        public void Configure(EntityTypeBuilder<GenericAddressType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(x => x.PropertyJson).WithMany(x => x.GenericAddressTypes).HasForeignKey(x => x.PropertyJsonId);
            builder.ConfigureLocalization();
            builder.ToTable("GenericAddressTypes");
        }
    }
}
