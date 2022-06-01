using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingPong.Models
{
    public class Tournament
    {
        public List<Match> Matches { get; set; }

        public List<Player> Players { get; set; }

    }
}
