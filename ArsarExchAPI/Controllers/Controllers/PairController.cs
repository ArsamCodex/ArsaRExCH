using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Interface.PropInterface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PairController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WalletInterface<double> _walletInterface;
        private readonly IProp _prop;
        public PairController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, WalletInterface<double> walletInterface, IProp prop)
        {
            _context = context;
            _userManager = userManager;
            _walletInterface = walletInterface;
            _prop = prop;
        }
        /*
         * Add new Curr3nfcy to page
         * */
        [HttpPost]
        public async Task<IActionResult> AddPairs(Pair ethereumPair)
        {
            try
            {
                await _context.Pair.AddAsync(ethereumPair);
                await _context.SaveChangesAsync();

                //Get All IDs of users
                var users = _userManager.Users.Select(u => u.Id).ToList();
                // var tasks = new List<Task>();

                // Create new wallets for all users
                foreach (var userId in users)
                {
                    //Eth addres is same here to not make but copy maded eth wallet in db
                    var x = await _context.Wallet.FirstOrDefaultAsync(c => c.ApplicationUserId == userId && c.PairName == "ETH");

                    if (x == null)
                    {
                        continue; // Skip creating the wallet for this user if their existing wallet is missing.
                    }
                    var ad = x.Adress;
                    var seed = x.SeedPhrase;
                    var privateKey = x.PrivateKey;
                    var walletEntity = new Model.Wallet
                    {
                        UserIDSec = userId,
                        ApplicationUserId = userId,
                        PairName = ethereumPair.PaiName,
                        Adress = ad,
                        Amount = 0,
                        SeedPhrase = seed,
                        CurrentPrice = 0,
                        PrivateKey = privateKey,
                        Network = "ETH"
                    };

                    // Save the wallet entity to the database
                    await _context.Wallet.AddAsync(walletEntity);
                    await _context.SaveChangesAsync();
                }

                // Wait for all wallet creation tasks to complete
                //   await Task.WhenAll(tasks);

                return Ok(ethereumPair);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPairs()
        {
            try
            {
                var pairs = await _context.Pair.ToListAsync();
                return Ok(pairs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /*Use this method  for creation of wallets , in very begining in first step 
         * */
        [HttpGet("GetPairsToCreateWallets")]
        public async Task<IActionResult> GetPairsToCreateWallets()
        {
            try
            {
                var pairs = await _context.Pair
                    .Select(p => new { p.PaiName, p.NetworkName })
                    .ToListAsync();

                return Ok(pairs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
   

    }
}
