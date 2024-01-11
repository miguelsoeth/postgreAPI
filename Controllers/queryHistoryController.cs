using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using postgreAPI.Data;
using postgreAPI.Models;

namespace postgreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class queryHistoryController : ControllerBase
    {
        private readonly DataContext _context;

        public queryHistoryController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<queryHistoryModel>>> GetQueryHistory()
        {
            var history = await _context.queries.OrderByDescending(q => q.queryDate).Take(100).ToListAsync();

            if (history is null)
            {
                return NotFound("Historico vazio.");
            }
                
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<List<queryHistoryModel>>> AddQueryHistory(queryHistoryModel qh)
        {
            _context.queries.Add(qh);
            await _context.SaveChangesAsync();

            return Ok(qh);
        }
    }
}
