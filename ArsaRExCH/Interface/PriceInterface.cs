namespace ArsaRExCH.Interface
{
    public interface PriceInterface
    {
        Task<double> GetBtcPriceFromBinance();
        Task<double> GetEthPriceFromBinance();
    }
}
