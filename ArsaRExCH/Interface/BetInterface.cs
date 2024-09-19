using ArsaRExCH.Model;
using ArsaRExCH.DTOs;

namespace ArsaRExCH.Interface
{
    public interface BetInterface
    {
        public  Task SaveBet(Bet bet);
        Task<string> Generatesha256();
        Task<Bet> GetBetBySha(string sha);
        Task<List<Bet>> GetBetsByUseId(string useId);
        Task CalculateBetResault(DateTime time,string id);
        Task<UserAnalyticsDTO> UserTradeAnalytics(string id);
        Task<string> GetClientIpAddress();
    }
}
