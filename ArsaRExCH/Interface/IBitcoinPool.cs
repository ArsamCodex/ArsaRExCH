using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface IBitcoinPool
    {
        Task InitBitcoinPool(BitcoinPool bitcoinPool);
        Task EditPool(BitcoinPool bitcoinPool);
    }
}
