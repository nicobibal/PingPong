using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PingPong.Models;

namespace PingPong.Controllers
{
    [Route("matches")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly MatchContext _context;

        public MatchesController(MatchContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatchs()
        {
            List<int> ids = new List<int>();
            List<Player> players = new List<Player>();

            var connectionString = "server=mysql-pingpongbdd.alwaysdata.net; port=3306; user=271403; password=-PingPong-; database=pingpongbdd_database";
            //je me connecte à la bdd
            MySqlConnection cnn = new MySqlConnection(connectionString);
            cnn.Open();
            //Je crée une requête sql
            string sql = @"SELECT * FROM Matches";
            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<Match>();
            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                ids.Add((int)reader["p1"]);
                ids.Add((int)reader["p2"]);
                var m = new Match()
                {
                    id = Convert.ToInt32(reader["id"]),
                    p1 = null,
                    p2 = null,
                    scoreP1 = (int)reader["scoreP1"],
                    scoreP2 = (int)reader["scoreP2"]
                };
                maListe.Add(m);
            }
            cnn.Close();
            for(int i = 0; i< ids.Count(); i++)
            {
                cnn = new MySqlConnection(connectionString);
                cnn.Open();
                //Je crée une requête sql
                sql = @"SELECT * FROM Player where id = " + ids[i];
                //Executer la requête sql, donc créer une commande
                cmd = new MySqlCommand(sql, cnn);
                reader = cmd.ExecuteReader();
                reader.Read();
                Player p = new Player()
                {
                    id = Convert.ToInt32(reader["id"]),
                    lastName = reader["lastName"].ToString(),
                    firstName = reader["firstName"].ToString(),
                    nationality = reader["nationality"].ToString()
                };

                players.Add(p);

                cnn.Close();
            }

            for(int i =0; i < maListe.Count(); i++)
            {
                maListe[i].p1 = players[i*2];
                maListe[i].p2 = players[i*2+1];
            }

            return maListe;
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(long id)
        {
            var match = await _context.Matchs.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(long id, Match match)
        {
            if (id != match.id)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Matches
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Matchs.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMatch), new { id = match.id }, match);
        }


   


        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Match>> DeleteMatch(long id)
        {
            var match = await _context.Matchs.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matchs.Remove(match);
            await _context.SaveChangesAsync();

            return match;
        }

        private bool MatchExists(long id)
        {
            return _context.Matchs.Any(e => e.id == id);
        }
    }
}
