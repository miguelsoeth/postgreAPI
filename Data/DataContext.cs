using Microsoft.EntityFrameworkCore;
using postgreAPI.Models;

namespace postgreAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<queryHistoryModel> queries { get; set; }
    }
}
