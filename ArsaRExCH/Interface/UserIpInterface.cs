using ArsaRExCH.DTOs;

namespace ArsaRExCH.Interface
{
    public interface UserIpInterface
    {
        Task<string> GetPublicIpAddress();
        Task<string> CheckIfIpIsBanned(string userIpAddress);
        string ExtractFirstPart(string ipAddress);
        /*This method returns list of maxinzetdto 
         * of most inzet u user whi price most inzet are and total 
         * bet
         */
        Task<List<MaxInzetResultDTO>> CalculateMaxInzetAsync();
        Task<string> GetPublicIpAddressWhitCountryName();
    }
}
