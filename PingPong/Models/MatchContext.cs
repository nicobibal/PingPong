using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PingPong.Models
{
    public class MatchContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public MatchContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public DbSet<Match> Matchs { get; set; }
    }
}