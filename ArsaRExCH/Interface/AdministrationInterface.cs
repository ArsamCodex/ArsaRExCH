using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface AdministrationInterface
    {
        Task<BanedCountries> AddBannCountries();
        Task<List<BanedCountries>> GetAllBannedCountriesInDatabase();
        Task<BanedCountries> RemoveBannedCuntries();
        Task<BanedCountries> EditBannedCountries();
        
    }
}
