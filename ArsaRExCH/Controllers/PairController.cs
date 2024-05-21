using ArsaRExCH.Data;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArsaRExCH.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PairController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddPairs(Pair pair)
        {
            try
            {
                await _context.Pair.AddAsync(pair);
                await _context.SaveChangesAsync();
                return Ok(pair);
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
