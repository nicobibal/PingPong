using MySql.Data.MySqlClient;
using PingPong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingPong.Controllers
{
    public class BDDCom
    {

        public static List<Player> GetPlayers()
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

        public static Player GetPlayer(int id)
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

        public static List<Match> GetMatches()
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
            for (int i = 0; i < ids.Count(); i++)
            {
                players.Add(BDDCom.GetPlayer(ids[i]));
            }

            for (int i = 0; i < maListe.Count(); i++)
            {
                maListe[i].p1 = players[i * 2];
                maListe[i].p2 = players[i * 2 + 1];
            }

            return maListe;
        }
    
        public static Match GetMatch(int id)
        {
            var l = GetMatches();
            for(int i=0; i < l.Count(); i++)
            {
                var m = l[i];
                if (m.id == id)
                    return m;
            }
            return null;
        }

    }
}
