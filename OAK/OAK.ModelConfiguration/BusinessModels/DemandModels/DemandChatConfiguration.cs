using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAK.Model.BusinessModels.DemandModels;

namespace OAK.ModelConfiguration.BusinessModels.DemandModels
{
    public class DemandChatConfiguration : IEntityTypeConfiguration<DemandChat>
    {
        public void Configure(EntityTypeBuilder<DemandChat> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(o => o.Demand).WithOne(w => w.DemandChat).HasForeignKey<DemandChat>(fk => fk.DemandId);
            builder.HasOne(o => o.Account).WithOne(w => w.DemandChat).HasForeignKey<DemandChat>(fk => fk.FromAccountId);
            builder.HasOne(o => o.Account).WithOne(w => w.DemandChat).HasForeignKey<DemandChat>(fk => fk.ToAccountId);
            builder.ToTable("DemandChats");
        }
    }
}