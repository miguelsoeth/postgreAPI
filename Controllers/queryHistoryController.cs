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
        public async Task<ActionResult<List<queryHistory>>> GetQueryHistory()
        {
            var history = await _context.queries.ToListAsync();
            return Ok(history);
        }
    }
}
