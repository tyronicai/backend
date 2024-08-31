namespace OAK.ModelConfiguration.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;
    using OAK.ModelConfiguration.BaseModels;
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {

            builder.ConfigureExLocalization();
            builder.ToTable("Role");
        }
    }
}