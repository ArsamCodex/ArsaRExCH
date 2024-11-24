using ArsaRExCH.DTOs;
using ArsaRExCH.Model.Prop;
using Microsoft.AspNetCore.Mvc;

namespace ArsaRExCH.Interface.PropInterface
{
    public interface IProp
    {
        //Make one generic method for all save data where t is class
        Task<T> SaveObject<T>(T propTrade) where T : class;
        Task DeleteObjectById<T>(object id) where T : class;
        Task EditObject<T>(object id, T updatedEntity) where T : class;
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<BinancePrepetualPriceDTO> GetBTCPerpetualPriceAsync();
        Task<decimal> GetBalanceForAuthenticatedUser();
        Task<bool> IsUserAuthenticated(string userId);
        Task<decimal> GetBalance(string id);
        Task<List<PrepPairDTO>> GetPricesForPairs([FromQuery] List<string> pairs);
    }
}
