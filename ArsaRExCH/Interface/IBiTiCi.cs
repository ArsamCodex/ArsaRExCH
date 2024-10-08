namespace ArsaRExCH.Interface
{
    public interface IBiTiCi
    {
        Task<string> WithdrawBtcAsync(string privateKey, string recipientAddress, decimal amount);

    }
}
