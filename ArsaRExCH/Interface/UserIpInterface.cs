namespace ArsaRExCH.Interface
{
    public interface UserIpInterface
    {
         Task<string> GetPublicIpAddress();
    }
}
