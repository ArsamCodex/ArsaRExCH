using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WalletInterface _walletInterface;
        public PairController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, WalletInterface walletInterface)
        {
            _context = context;
            _userManager = userManager;
            _walletInterface = walletInterface;
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
                var tasks = new List<Task>();

                // Create new wallets for all users
                foreach (var userId in users)
                {
                    tasks.Add(_walletInterface.CreateETHWallet(userId, ethereumPair.PaiName));
                }

                // Wait for all wallet creation tasks to complete
                await Task.WhenAll(tasks);

                return Ok(ethereumPair);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
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
