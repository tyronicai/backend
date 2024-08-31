namespace OAK.ModelConfiguration.Core
{

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;
    using OAK.ModelConfiguration.BaseModels;
    public class AccountRoleConfiguration : IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(f => new { f.AccountId, f.RoleId });
            builder.ConfigureBase();
            builder.HasOne(x => x.Account).WithMany(x => x.AccountRoles).HasForeignKey(x => x.AccountId);
            builder.HasOne(x => x.Role).WithMany(x => x.AccountRoles).HasForeignKey(x => x.RoleId);
            builder.ToTable("AccountRole");
        }
    }
}
