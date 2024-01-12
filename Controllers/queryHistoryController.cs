using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using postgreAPI.Data;
using postgreAPI.Dtos;
using postgreAPI.Models;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace postgreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class queryHistoryController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public queryHistoryController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<queryHistoryModel>>> GetQueryHistory()
        {
            var history = await _context.queries.OrderByDescending(q => q.querydate).Take(100).ToListAsync();

            if (history is null)
            {
                return NotFound("Historico vazio.");
            }
                
            return Ok(history);
        }

        [HttpPost]
        public async Task<ActionResult<queryHistoryModel>> AddQueryHistoryDTO(queryHistoryResponse qh)
        {
            var connection = new NpgsqlConnection(_configuration.GetConnectionString("DatabaseConnection"));
            await connection.OpenAsync();
            var command = new NpgsqlCommand
            {
                Connection = connection,
                CommandText = "INSERT INTO public.queries (username, type, document, referredDate, interval) " +
                                  "VALUES (@username, @type, @document, @referredDate, @interval) RETURNING *"
            };
            command.Parameters.AddWithValue("username", qh.username);
            command.Parameters.AddWithValue("type", qh.type);
            command.Parameters.AddWithValue("document", qh.document);
            command.Parameters.AddWithValue("referredDate", qh.referredDate);
            command.Parameters.AddWithValue("interval", qh.interval);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    var result = new queryHistoryModel
                    {
                        id = Convert.ToInt32(reader["id"]), // Assuming "id" is the name of the column in the database
                        username = reader["username"].ToString(),
                        querydate = DateTime.UtcNow, // Assuming you want to set the querydate to the current UTC time
                        type = reader["type"].ToString(),
                        document = reader["document"].ToString(),
                        referreddate = Convert.ToDateTime(reader["referredDate"]),
                        interval = reader["interval"].ToString()
                    };
                    reader.Close();
                    return result;
                }
            }
            return NotFound();
        }
    }
}
