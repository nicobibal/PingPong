using Microsoft.EntityFrameworkCore;

namespace PingPong.Models
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options)
            : base(options)
        {

        }

        public DbSet<Match> Matchs { get; set; }
    }
}