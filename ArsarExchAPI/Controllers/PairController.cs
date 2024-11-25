
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArsarExchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
      [Authorize]
    public class PairController : ControllerBase
    {
        private readonly Data.ApplicationDbContext _context;
        private readonly UserManager<Data.ApplicationUser> _userManager;

        public PairController(Data.ApplicationDbContext context, UserManager<Data.ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]

        public async Task<IActionResult> Test()
        {
            var x = await _context.Pair.ToListAsync();
            return Ok(x); // Return the list inside an IActionResult
        }

        /*
         * Add new Curr3nfcy to page
         * */

        /*Use this method  for creation of wallets , in very begining in first step 
         * */
        /*
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
   

    }*/
    }

}