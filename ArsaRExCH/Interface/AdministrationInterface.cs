using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface AdministrationInterface
    {
        Task<BanedCountries> AddBannCountries();
        Task<BanedCountries> GetAllBannedCountriesInDatabase();
        Task<BanedCountries> RemoveBannedCuntries();
        Task<BanedCountries> EditBannedCountries();
        
    }
}
