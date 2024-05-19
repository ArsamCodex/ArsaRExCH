using ArsaRExCH.Data;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public WalletController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("Wallets/{id}")]
        public async Task<IActionResult> GetWallets(string id)
        {
            try
            {
                var user = await _context.Users
                 //   .Include(u => u.Wallets) // Ensure that User entity has a Wallets navigation property
                    .FirstOrDefaultAsync(u => u.UserInDbId == id);

                if (user == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }

                return Ok(user); // Return the wallets of the user
            }
            catch (Exception ex)
            {
                return BadRequest( $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("WalletsL/{id}")]
        public async Task<ActionResult<List<Wallet>>> GetWalletsListed(string id)
        {
            try
            {
                var user = await _context.Wallet
                   // .Include(u => u.Wallets) // Ensure that User entity has a Wallets navigation property
                    .FirstOrDefaultAsync(u => u.UserID == id);

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
    }
}
