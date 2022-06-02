using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingPong.Models
{
    public class Tournament
    {
        public int id { get; set; }
        public List<Match> matches { get; set; }

        public List<Player> players { get; set; }

    }
}
