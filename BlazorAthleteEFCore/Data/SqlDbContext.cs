using Microsoft.EntityFrameworkCore;

namespace BlazorAthleteEFCore.Data
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
           : base(options)
        {
        }
        public DbSet<Athlete> Athletes { get; set; }
    }
}
