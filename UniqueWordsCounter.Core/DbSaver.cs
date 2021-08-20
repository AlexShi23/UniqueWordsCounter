using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace UniqueWordsCounter.Core
{
    public class DbSaver
    {
        public static void SaveToDatabase(Dictionary<string, uint> items, string url)
        {
            string connectionString = @"URI=file:test.db";

            using var con = new SQLiteConnection(connectionString);
            con.Open();

            using var cmd = new SQLiteCommand(con);
            string tableName = url.Split(new string[] { "//" }, StringSplitOptions.None)[1].Replace("/", "_").Replace(".", "_");

            cmd.CommandText = $"DROP TABLE IF EXISTS {tableName}";
            cmd.ExecuteNonQuery();

            cmd.CommandText = $@"CREATE TABLE {tableName}(word TEXT PRIMARY KEY,count INT)";
            cmd.ExecuteNonQuery();

            foreach (var item in items)
            {
                cmd.CommandText = $"INSERT INTO {tableName}(word, count) VALUES('{item.Key}',{item.Value})";
                cmd.ExecuteNonQuery();
            }
        }
    }
}
