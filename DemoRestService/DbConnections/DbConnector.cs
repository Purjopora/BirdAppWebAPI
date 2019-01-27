using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace DemoRestService.DbConnections
{
    public class DbConnector
    {
        public static bool AddTowerToDB(Tower tower)
        {

            var server = "server ip here";
            var database = "testdb";
            var user = "username here";
            var password = "password here";
            var port = "3306";
            var sslM = "none";
            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "INSERT INTO towers (id, municipal, towername, longitudecoord, latitudecoord) VALUES('@id', '@municipal', '@towername', '@longitudecoord', '@latitudecoord')";
            
            query = query.Replace("@id", tower.id)
                .Replace("@municipal", tower.municipal)
                .Replace("@towername", tower.towername)
                .Replace("@longitudecoord", tower.longitudecoord)
                .Replace("@latitudecoord", tower.latitudecoord);
            
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                //throw
                return false;
            }
        }

        public static DataTable GetTowersFromDB(string municipal)
        {
            var server = "server ip here";
            var database = "testdb";
            var user = "username here";
            var password = "password here";
            var port = "3306";
            var sslM = "none";
            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "";
            if(municipal == null)
            {
                query = "SELECT * FROM testdb.towers";
            }
            else
            {
                query = "SELECT * FROM testdb.towers WHERE municipal = '@municipal'";
                query = query.Replace("@municipal", municipal);
            }

            List<String> towers = new List<String>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            MySqlCommand command = new MySqlCommand(query, connection);

            try
            {
                var dt = new DataTable();

                connection.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(command);
                da.Fill(dt);
                command.Dispose();
                connection.Close();
                return dt;
                
            }
            catch (Exception)
            {
                //throw
                return null;
            }
        }


    }
}