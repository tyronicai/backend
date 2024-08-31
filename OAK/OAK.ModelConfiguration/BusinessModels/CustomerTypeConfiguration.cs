namespace OAK.ModelConfiguration.BusinessModels
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.BusinessModels;
    using OAK.ModelConfiguration.BaseModels;

    public class CustomerTypeConfiguration : IEntityTypeConfiguration<CustomerType>
    {
        public void Configure(EntityTypeBuilder<CustomerType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);



            builder.ConfigureLocalization();
            builder.ToTable("CustomerType");
        }
    }
}
