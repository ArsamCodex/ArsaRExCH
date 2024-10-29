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
        Task AddAdminWarningMessage(AdminWarningMessage adminWarningMessage);
        Task<AdminWarningMessage> GetAdminWarningMessage(DateTime date);
        Task DeleteAdminWarning(string id);
        Task<string?> GetScheduledMessageAsync();
        Task<bool> CheckAdminSetupStatusAsync();
        Task<AdminSetupInit> EditAdminInit();
        Task EditBTCWalletExchangeAmountDecrease(string userID, double amount);
        Task EditBTCWalletExchangeAmountIncrease(string userID, double amount);
    }
}
