using ArsaRExCH.Data;
using ArsaRExCH.Interface;
using ArsaRExCH.InterfaceIMPL;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArsaRExCH.DTOs;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly WalletInterface _walletInterface;
        public WalletController(ApplicationDbContext context, WalletInterface walletInterface)
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
                                 .Where(w => w.User.Id == id)
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
                                 .Where(w => w.User.Id == id)
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

    }

  
}
