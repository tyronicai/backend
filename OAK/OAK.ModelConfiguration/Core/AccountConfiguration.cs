namespace OAK.ModelConfiguration.Core
{

    using Microsoft.EntityFrameworkCore;

    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using OAK.Model.Core;
    using OAK.ModelConfiguration.BaseModels;

    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(f => f.Password).IsRequired();

            builder.Property(f => f.LoginAttempts).IsRequired();

            builder.Property(x => x.IsCompanyOwner).HasDefaultValue(false);

            builder.Property(f => f.LastLoginDate).HasColumnType("timestamp");
            builder.Property(f => f.LastPasswordChangeDate).IsRequired().HasColumnType("timestamp");

            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.TempVerificationString).IsUnique();
            builder.HasIndex(x => x.PhoneNumber);

            builder.Property(f => f.IsEmailActivated).IsRequired();
            builder.Property(f => f.EmailActivationDate).HasColumnType("timestamp");
            builder.Property(f => f.TwoFactorAuthenticationEnabled).IsRequired();

            builder.HasMany(x => x.Comments).WithOne(x => x.Account).HasForeignKey(x => x.AccountId);

            builder.ConfigureBase();

            builder.ToTable("Account");
        }
    }
}