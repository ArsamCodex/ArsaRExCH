namespace ArsaRExCH.Interface
{
    public interface UserIpInterface
    {
         Task<string> GetPublicIpAddress();
        Task<string> CheckIfIpIsBanned(string userIpAddress);
        string ExtractFirstTwoDigits(string ipAddress);
    }
}
