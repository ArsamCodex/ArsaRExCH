using ArsaRExCH.Data;
using ArsaRExCH.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArsaRExCH
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
    }
}
