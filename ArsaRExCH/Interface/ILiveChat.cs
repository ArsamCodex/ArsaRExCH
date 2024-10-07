using ArsaRExCH.Model;

namespace ArsaRExCH.Interface
{
    public interface ILiveChat
    {
        Task SaveMessage(LiveChat liveChat);
        Task<List<LiveChat>> GetAllMessages();
    }
}
