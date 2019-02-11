using DemoRestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;

namespace DemoRestService.DbConnections
{
    public class DbConnector
    {


        public static string server = "mytestdb.c98qcb3crjgp.us-east-2.rds.amazonaws.com";
        static string database = "testdb";
        static string user = "Juupperi";
        static string password = "12344321";
        static string port = "3306";
        static string sslM = "none";
        private static int FRESH_BIRD_TIMESPAN = 1; //24 hours for now

        public static bool AddTowerToDB(Tower tower)
        {
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

            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "";
            if (municipal == null)
            {
                query = "SELECT * FROM testdb.towers ORDER BY municipal ASC";
            }
            else
            {
                query = "SELECT * FROM testdb.towers WHERE municipal = '@municipal' ORDER BY  municipal ASC";
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


        public static DataTable GetSpeciesFromDB()
        {

            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "";

            query = "SELECT * FROM testdb.species ORDER BY speciename ASC";



            List<String> species = new List<String>();
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


        //---------------------------------------------------------------
        //ACCOUNT MANAGEMENT STUFF BELOW

        public static DataTable GetUserFromDB(string username)
        {

            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "";

            query = "SELECT * FROM testdb.users WHERE username = '@username'";
            query = query.Replace("@username", username);



            List<String> species = new List<String>();
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

        public static bool AddUserToDB(User usercredentials)
        {
            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            //var query = "INSERT INTO users (username, passwordhash) VALUES('@username', '@password')";



            //ADD user to users-table
            try
            {
                var query = "INSERT INTO users (username, passwordhash) VALUES('@username', '@password')";
                query = query.Replace("@username", usercredentials.username)
                    .Replace("@password", usercredentials.passwordhash);
                MySqlConnection connection = new MySqlConnection(connectionString);
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

        //---------------------------------------------
        //ADD NEW SIGHTINGS TO DB

        public static bool UpdateSightingsToDB(BirdSighting sighting)
        {
            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "INSERT INTO sightings (username, specie, longitudecoord, latitudecoord, comment, timestamp)" +
                " VALUES('@username', '@specie', @longitudecoord, @latitudecoord,'@comment',@timestamp)";
            query = query.Replace("@username", sighting.username)

                //ADD empty comment if none provided


                .Replace("@specie", sighting.specie)
                //.Replace("@longitudecoord", sighting.longitudecoord)
                //.Replace("@latitudecoord", sighting.latitudecoord)
                .Replace("@comment", sighting.comment);
            //.Replace("@timestamp", sighting.timestamp.ToString());

            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@timestamp", DateTime.Now);
                command.Parameters.AddWithValue("@longitudecoord", sighting.longitudecoord);
                command.Parameters.AddWithValue("@latitudecoord", sighting.latitudecoord);

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




        public static DataTable GetSightingsFromDB(string username)
        {

            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            var query = "SELECT * FROM testdb.sightings WHERE username = '@user'";

            query = query.Replace("@user", username);



            List<String> species = new List<String>();
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

        public static DataTable GetSightingsForBird(string bird)
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM testdb.bird" + bird+ ";";
            return executeQuery(query);
        }
        public static DataTable getTables()
        {
            DataTable dt = new DataTable();
            string query = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'testdb';";

            return executeQuery(query);
        }
        private void clearTable(string birdTableName)
        {

        }
        public static bool updateSightingsTransaction()
        {
            DateTime now = DateTime.Now;
            DataTable resultdt = DbConnector.GetSpeciesFromDB();
            if (resultdt == null)
            {
                return false;
            }

            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = connection.CreateCommand())
                {
                    MySqlTransaction transaction;

                    transaction = connection.BeginTransaction();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    try
                    {
                        foreach (DataRow row in resultdt.Rows)
                        {
                            String birdTableName = "testdb.bird" + row["speciename"].ToString();
                            birdTableName = birdTableName.Replace("-", "");
                            String clearQuery = "DELETE from " + birdTableName + ";";
                            command.CommandText = clearQuery;
                            command.ExecuteNonQuery();


                            string sightingsQuery = "SELECT * FROM testdb.sightings WHERE specie = '@name'";
                            sightingsQuery = sightingsQuery.Replace("@name", row["speciename"].ToString());
                            DataTable birds = executeQuery(sightingsQuery);
                            if (birds == null)
                            {
                                return false;
                            }
                            var freshBirds = new List<BirdSighting>();
                            foreach (DataRow birbRow in birds.Rows)
                            {
                                DateTime time = Convert.ToDateTime(birbRow["timestamp"]);
                                TimeSpan span = now.Subtract(time);
                                double lon = Convert.ToDouble(birbRow["longitudecoord"]);
                                double lat = Convert.ToDouble(birbRow["latitudecoord"]);
                                if (span.CompareTo(TimeSpan.FromDays(FRESH_BIRD_TIMESPAN)) < 0)
                                {
                                    var sighting = new BirdSighting
                                    {
                                        username = "",
                                        specie = row["speciename"].ToString(),
                                        longitudecoord = lon,
                                        latitudecoord = lat,
                                        comment = "",
                                        timestamp = now
                                    };
                                    freshBirds.Add(sighting);
                                }
                            }

                            string insertQuery = "INSERT INTO " + birdTableName + " VALUES(@latitude, @longtitude);";
                            command.CommandText = insertQuery;
                            command.Parameters.Add("@latitude", MySqlDbType.Float);
                            command.Parameters.Add("@longtitude", MySqlDbType.Float);
                            foreach (BirdSighting bird in freshBirds)
                            {
                                command.Parameters["@latitude"].Value = bird.latitudecoord;
                                command.Parameters["@longtitude"].Value = bird.longitudecoord;
                                command.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception e)
                    {
                        try
                        {
                            transaction.Rollback();
                            return false;
                        }
                        catch (MySqlException ex)
                        {
                            return false;
                        }
                    }
                }
            }
        }

        private static DataTable executeQuery(string query)
        {
            var connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                try
                {
                    var dt = new DataTable();

                    connection.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    da.Fill(dt);
                    return dt;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
    }
}