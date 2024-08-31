namespace OAK.Model.Core
{
    using OAK.Model.BaseModels;
    //public class PostCodeData : LocalizationModelBase
    public class PostCodeData : ModelBase
    {
        public int Id { get; set; }
        public string IsoCountryCode { get; set; }  // country code      : iso country code, 2 characters
        public string PostCode { get; set; }      // postal code       : varchar(20)
        public string PlaceName { get; set; }      //place name        : varchar(180)
        public string AdminName1 { get; set; }      //           : 1. order subdivision(state) varchar(100)
        public string AdminCode1 { get; set; }      // admin code1       : 1. order subdivision(state) varchar(20)
        public string AdminName2 { get; set; }      // admin name2       : 2. order subdivision(county/province) varchar(100)
        public string AdminCode2 { get; set; }      // admin code2       : 2. order subdivision(county/province) varchar(20)
        public string AdminName3 { get; set; }      // admin name3       : 3. order subdivision(community) varchar(100)
        public string AdminCode3 { get; set; }      // admin code3       : 3. order subdivision(community) varchar(20)
        public double Lattitude { get; set; }      //latitude          : estimated latitude(wgs84)
        public double Longitude { get; set; }      //longitude         : estimated longitude(wgs84)
        public int Accuracy { get; set; }      //accuracy          : accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape

        public virtual Country Country { get; set; }

    }
}