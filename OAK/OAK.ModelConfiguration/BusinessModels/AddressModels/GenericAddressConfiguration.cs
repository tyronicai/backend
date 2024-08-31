namespace OAK.ModelConfiguration.BusinessModels.AddressModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels.AddressModels;
    public class GenericAddressConfiguration : IEntityTypeConfiguration<GenericAddress>
    {
        public void Configure(EntityTypeBuilder<GenericAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PostCode).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Street).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Town).HasMaxLength(255);

            //builder.HasOne(x => x.City).WithMany(x => x.CustomerAddresses).HasForeignKey(x => x.CityId);
            builder.HasOne(x => x.GenericAddressType).WithMany(x => x.GenericAddresses).HasForeignKey(x => x.GenericAddressTypeId);

            builder.ToTable("GenericAddresses");
        }
    }
}
