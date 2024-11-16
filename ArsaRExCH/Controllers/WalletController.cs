using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.InterfaceIMPL;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsaRExCH.DTOs;
using ArsaRExCH.Model.Prop;
using Newtonsoft.Json;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly WalletInterface<double> _walletInterface;
        public WalletController(ApplicationDbContext context, WalletInterface<double> walletInterface)
        {
            _context = context;
            _walletInterface = walletInterface;
        }
        [HttpGet("Wallets/{id}")]
        public async Task<IActionResult> GetWallets(string id)
        {
            try
            {
                var user = await _context.Wallet
                                 // .Include(u => u.Wallets)
                                 .Where(w => w.UserIDSec == id)
                                    .ToListAsync();

                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(user); // Return the wallets of the user
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("WalletsL/{id}")]
        public async Task<ActionResult<List<Wallet>>> GetWalletsListed(string id)
        {
            try
            {
                var user = await _context.Wallet
                                 // .Include(u => u.Wallets)
                                 .Where(w => w.UserIDSec == id)
                                    .ToListAsync();




                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(user); // Return the wallets of the user
            }
            catch (Exception ex)
            {
                return BadRequest($"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("GetWalletBalanceBTC/{adres}")]
        public async Task<ActionResult<decimal>> GetBalanceWalletBTC(string adres)
        {
            var x = await _walletInterface.GetBalanceFromBlockCypherAsync(adres);
            return x;
        }
        [HttpGet("PriceList")]
        public async Task<List<PrepPairDTO>> GetPricesForPairs([FromQuery] List<string> pairs)
        {
            using HttpClient client = new HttpClient();
            string url = "https://fapi.binance.com/fapi/v1/ticker/price";

            // Fetch JSON response from Binance API
            var response = await client.GetStringAsync(url);
            Console.WriteLine(response); 
            // Deserialize JSON to List<PrepPir>
            var allPrices = JsonConvert.DeserializeObject<List<PrepPairDTO>>(response);

            // Filter the result based on the specified pairs
            return allPrices
                .Where(p => pairs.Contains(p.PairName))   // Filter to only include the specified pairs
                .ToList();
        }

    }



}
