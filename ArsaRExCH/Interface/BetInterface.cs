using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface BetInterface
    {
        public  Task SaveBet(Bet bet);
        Task<string> Generatesha256();
        Task<Bet> GetBetBySha(string sha);
       
    }
}
