using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PingPong.Models;

namespace PingPong.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerContext _context;

        public PlayersController(PlayerContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var connectionString = "server=mysql-pingpongbdd.alwaysdata.net; port=3306; user=271403; password=-PingPong-; database=pingpongbdd_database";
            //je me connecte à la bdd
            MySqlConnection cnn = new MySqlConnection(connectionString);
            cnn.Open();
            //Je crée une requête sql
            string sql = @"SELECT * FROM Player";
            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            var reader = cmd.ExecuteReader();
            var maListe = new List<Player>();
            //Récupérer le retour, et le transformer en objet
            while (reader.Read())
            {
                var p = new Player()
                {
                    id = Convert.ToInt32(reader["id"]),
                    lastName = reader["lastName"].ToString(),
                    firstName = reader["firstName"].ToString(),
                    nationality = reader["nationality"].ToString()
                };
                maListe.Add(p);
            }
            cnn.Close();
            return maListe;
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {
            var connectionString = "server=mysql-pingpongbdd.alwaysdata.net; port=3306; user=271403; password=-PingPong-; database=pingpongbdd_database";
            //je me connecte à la bdd
            MySqlConnection cnn = new MySqlConnection(connectionString);
            cnn.Open();
            //Je crée une requête sql
            string sql = @"SELECT * FROM Player where id = " + id;
            //Executer la requête sql, donc créer une commande
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Player p = new Player()
            {
                id = Convert.ToInt32(reader["id"]),
                lastName = reader["lastName"].ToString(),
                firstName = reader["firstName"].ToString(),
                nationality = reader["nationality"].ToString()
            };

            cnn.Close();
            return p;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(long id, Player player)
        {
            if (id != player.id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlayer), new { id = player.id }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Player>> DeletePlayer(long id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return player;
        }

        private bool PlayerExists(long id)
        {
            return _context.Players.Any(e => e.id == id);
        }
    }
}
