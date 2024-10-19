using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface ITrade

    {
        Task SaveTrade(Trade trade);
    }
}
