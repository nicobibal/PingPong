

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PingPong.Models
{
    public class PlayerContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public PlayerContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Player> Players { get; set; }
    }
}