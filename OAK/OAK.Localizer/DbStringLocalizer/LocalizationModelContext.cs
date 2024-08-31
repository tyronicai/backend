namespace OAK.Localizer.DbStringLocalizer
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using OAK.Model.Core;
    using OAK.Model.Localization;
    using OAK.ModelConfiguration.LocalizationConfiguration;
    using OAK.ModelConfiguration.Core;
    using System;
    using System.Linq;
    // >dotnet ef migrations add LocalizationMigration
    public class LocalizationModelContext : DbContext
    {
        private readonly string _schema;

        public LocalizationModelContext(DbContextOptions<LocalizationModelContext> options, IOptions<SqlContextOptions> contextOptions) : base(options)
        {
            _schema = contextOptions.Value.SqlSchemaName;
        }

        public LocalizationModelContext(DbContextOptions<LocalizationModelContext> options, string schema) : base(options)
        {
            _schema = schema;
        }

        public DbSet<LocalizationKey> LocalizationKeys { get; set; }
        public DbSet<LocalizationText> LocalizationTexts { get; set; }
        public DbSet<Language> Languages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (!string.IsNullOrEmpty(_schema))
                builder.HasDefaultSchema(_schema);

            builder.ApplyConfiguration(new LocalizationKeyConfiguration());
            builder.ApplyConfiguration(new LocalizationTextConfiguration());
            builder.ApplyConfiguration(new LanguageConfiguration());
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            //updateUpdatedProperty<LocalizationRecord>();            
            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = ChangeTracker.Entries().ToList();
            foreach (var entity in changedEntriesCopy)
            {
                Entry(entity.Entity).State = EntityState.Detached;
            }
        }
    }
}