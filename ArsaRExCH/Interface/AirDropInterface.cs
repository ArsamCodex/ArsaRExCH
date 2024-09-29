using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface AirDropInterface
    {
        Task<bool> SaveDrop(AirDrop airDrop);
        Task<AirDrop> GetAirDropById(string id);
        Task<bool> IncrementAndSaveAirDrop(string userId);
        Task<int> AirDropWalletBalance(string id);
        Task<int> AirDropDailyClick(string id);
        Task<bool> SaveAirDropFaq(AirDropFaq airDropFaq);
    }
}
