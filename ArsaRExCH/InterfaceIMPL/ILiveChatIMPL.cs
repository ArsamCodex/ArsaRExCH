using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.InterfaceIMPL
{
    public class ILiveChatIMPL : ILiveChat
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbContextFactory;
        public ILiveChatIMPL(IDbContextFactory<ApplicationDbContext> _dbContextFactory)
        {
            dbContextFactory = _dbContextFactory;
        }
        public async  Task<List<LiveChat>> GetAllMessages()
        {
            try
            {
                using var _context = dbContextFactory.CreateDbContext();
                return await _context.lifeChat.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Not list founnd or error ", ex);
            }
        }

        public async Task SaveMessage(LiveChat liveChat)
        {
            try
            {
                using var _context = dbContextFactory.CreateDbContext();
                await _context.lifeChat.AddAsync(liveChat);
            }
            catch (Exception ex)
            {
                throw new Exception("Error to save Livechat Object ", ex);
            }
        }
    }
}
