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

        public static string server = "";
        static string database = "";
        static string user = "";
        static string password = "";
        static string port = "3306";
        static string sslM = "none";

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
            if(municipal == null)
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
            var query = "INSERT INTO users (username, password) VALUES('@username', '@password')";

            query = query.Replace("@username", usercredentials.username)
                .Replace("@password", usercredentials.password);
                

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


    }
    
    public class PasswordHash
    {

        /* CreateHash palauttaa stringin, jossa algorithm:iterations:hashSize:salt:hash
         * Algorithm = SHA-1
         * Hash käyttää PBKDF2
         * 
         * VerifyPassword palauttaa true, jos salasana vastaa hashia
         * 
         * */

        // These constants may be changed without breaking existing hashes.
        public const int SALT_BYTES = 64;
        public const int HASH_BYTES = 18;
        public const int PBKDF2_ITERATIONS = 64000;

        // These constants define the encoding and may not be changed.
        public const int HASH_SECTIONS = 5;
        public const int HASH_ALGORITHM_INDEX = 0;
        public const int ITERATION_INDEX = 1;
        public const int HASH_SIZE_INDEX = 2;
        public const int SALT_INDEX = 3;
        public const int PBKDF2_INDEX = 4;

        public static string CreateHash(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[SALT_BYTES];
            try
            {
                using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
                {
                    csprng.GetBytes(salt);
                }
            }
            catch (Exception)
            {
                // 
                return null;
            }

            byte[] hash = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTES);

            // format: algorithm:iterations:hashSize:salt:hash
            String parts = "sha1:" +
                PBKDF2_ITERATIONS +
                ":" +
                hash.Length +
                ":" +
                Convert.ToBase64String(salt) +
                ":" +
                Convert.ToBase64String(hash);
            return parts;
        }

        public static bool VerifyPassword(string password, string parts)
        {
            char[] delimiter = { ':' };
            string[] split = parts.Split(delimiter);

            int iterations = 0;
            iterations = Int32.Parse(split[ITERATION_INDEX]);

            byte[] salt = null;
            try
            {
                salt = Convert.FromBase64String(split[SALT_INDEX]);
            }
            catch (Exception)
            {
                //
                return false;
            }

            byte[] hash = null;
            try
            {
                hash = Convert.FromBase64String(split[PBKDF2_INDEX]);
            }
            catch (Exception)
            {
                //
                return false;
            }

            int storedHashSize = 0;
            storedHashSize = Int32.Parse(split[HASH_SIZE_INDEX]);

            byte[] testHash = PBKDF2(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt))
            {
                pbkdf2.IterationCount = iterations;
                return pbkdf2.GetBytes(outputBytes);
            }
        }
    }
}