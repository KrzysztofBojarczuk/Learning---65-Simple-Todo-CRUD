using Microsoft.EntityFrameworkCore;
using TodowebApi.Models;

namespace TodowebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Todo> Todos { get; set; }
    }
}
