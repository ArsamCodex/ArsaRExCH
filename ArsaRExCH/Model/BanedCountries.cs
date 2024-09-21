namespace ArsaRExCH.Model
{
    public class BanedCountries
    {
        //Admin table to set banned coutries to avoid log inn
        public int BanedCountriesId { get; set; }
        public string? IpAdressToBann { get; set; }
        public string? CountryName { get; set; }
    }
}
