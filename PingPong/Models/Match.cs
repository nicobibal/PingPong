using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingPong.Models
{
    public class Match
    {
        public long id { get; set; }
        public Player P1 { get; set; }
        public Player P2 { get; set; }
        public int scoreP1 { get; set; }
        public int scoreP2 { get; set; }
    }
}
