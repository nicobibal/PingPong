using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingPong.Models
{
    public class Match
    {
        public int id { get; set; }
        public Player p1 { get; set; }
        public Player p2 { get; set; }
        public int scoreP1 { get; set; }
        public int scoreP2 { get; set; }
    }
}
