namespace OAK.ModelConfiguration.Core
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using OAK.Model.Core;

    public class PostCodeDataConfiguration : IEntityTypeConfiguration<PostCodeData>
    {
        public void Configure(EntityTypeBuilder<PostCodeData> builder)
        {


            // public int Id { get; set; }
            // public string IsoCountryCode { get; set; }  // country code      : iso country code, 2 characters
            // public string PostCode { get; set; }      // post code       : varchar(20)
            // public string PlaceName { get; set; }      //place name        : varchar(180)
            // public string AdminName1 { get; set; }      //           : 1. order subdivision(state) varchar(100)
            // public string AdminCode1 { get; set; }      // admin code1       : 1. order subdivision(state) varchar(20)
            // public string AdminName2 { get; set; }      // admin name2       : 2. order subdivision(county/province) varchar(100)
            // public string AdminCode2 { get; set; }      // admin code2       : 2. order subdivision(county/province) varchar(20)
            // public string AdminName3 { get; set; }      // admin name3       : 3. order subdivision(community) varchar(100)
            // public string AdminCode3 { get; set; }      // admin code3       : 3. order subdivision(community) varchar(20)
            // public double Lattitude { get; set; }      //latitude          : estimated latitude(wgs84)
            // public double Longitude { get; set; }      //longitude         : estimated longitude(wgs84)
            // public int Accuracy { get; set; }      //accuracy          : accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape

            //builder.ConfigureLocalization();
            builder.HasKey(k => k.Id);
            builder.HasIndex(i => i.IsoCountryCode);
            builder.HasOne(c => c.Country).WithMany(p => p.PostCodes).HasForeignKey(f => f.IsoCountryCode).HasPrincipalKey(p => p.IsoCode2);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.IsoCountryCode).IsRequired().IsFixedLength().HasMaxLength(2);
            builder.Property(p => p.PostCode).IsRequired().HasMaxLength(20);
            builder.Property(p => p.PlaceName).IsRequired().HasMaxLength(180);
            builder.Property(p => p.AdminName1).HasMaxLength(100);
            builder.Property(p => p.AdminCode1).HasMaxLength(20);
            builder.Property(p => p.AdminName2).HasMaxLength(100);
            builder.Property(p => p.AdminCode2).HasMaxLength(20);
            builder.Property(p => p.AdminName3).HasMaxLength(100);
            builder.Property(p => p.AdminCode3).HasMaxLength(20);
            builder.Property(p => p.Lattitude).IsRequired().HasColumnType("double precision");
            builder.Property(p => p.Longitude).IsRequired().HasColumnType("double precision");
            builder.Property(p => p.Accuracy).IsRequired();
            builder.ToTable("PostCodeData");
        }
    }
}