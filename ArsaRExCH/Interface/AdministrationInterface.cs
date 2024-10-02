using ArsaRExCH.Data;
using ArsaRExCH.Model;
using System.Security.Claims;

namespace ArsaRExCH.Interface
{
    public interface AdministrationInterface
    {
        Task AddBannCountries(BanedCountries banedCountries);
        Task<List<BanedCountries>> GetAllBannedCountriesInDatabase();
        Task<bool> RemoveBannedCuntries(int banbId);
        Task<List<ApplicationUser>> AllUsers();
        Task<ApplicationUser> GetUserById(string userId);
        Task<List<UserDatesRecord>> GetAllUserDates(string userID);
        Task<List<AirDropFaq>> GetAllAirDropFaq();
        Task<List<string>> GetUserRolesAsync(ClaimsPrincipal user);


    }
}
