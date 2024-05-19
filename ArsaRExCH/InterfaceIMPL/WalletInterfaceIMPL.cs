using ArsaRExCH.Data;
using ArsaRExCH.Interface;

namespace ArsaRExCH.InterfaceIMPL
{
    public class WalletInterfaceIMPL : WalletInterface
    {
        private readonly ApplicationDbContext _context;

        public WalletInterfaceIMPL(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateBNBWallet(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateBTCWallet(string id)
        {
            throw new NotImplementedException();
        }

        public Task CreateETHWallet(string id)
        {
            throw new NotImplementedException();
        }
    }
}
