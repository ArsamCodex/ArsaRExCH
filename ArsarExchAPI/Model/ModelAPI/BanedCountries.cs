namespace ArsarExchAPI.Model.ModelAPI
{
    public class BanedCountries
    {
        //Admin table to set banned coutries to avoid log inn
        /*
         * This class is working like this if you want to add countries bann to database 
         * you go to this site to see ip range of country https://lite.ip2location.com/ip-address-ranges-by-country?lang=en_US
         * for example uk ip 62.102.95.0	  public ( 1 of ) here you put onlly 62 in database its unique range belongs onlly
         * to uk , or letme saay first digits from left to . u ut the digits to database and NOT whole Ip
         * program now calculates first 2 digit i will improve it to first 3 digit 
         * 
         * 
         */
        public int BanedCountriesId { get; set; }
        public string? IpAdressToBann { get; set; }
        public string? CountryName { get; set; }
    
    }
}
