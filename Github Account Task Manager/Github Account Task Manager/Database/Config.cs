using Github_Account_Task_Manager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Github_Account_Task_Manager.Database
{
    internal class Config
    {
        public static string DatabaseFilename = "MyApplication.db3";
        private static SQLiteAsyncConnection _database;
        
        public Config(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Models.User>().Wait();
            _database.CreateTableAsync<Models.Task>().Wait();
        }

        public static SQLiteAsyncConnection GetDatabaseConnection()
        {
            return _database;
        }

        private static Config database;
        internal static Config Database
        {
            get
            {
                if (database == null)
                {
                    database = new Config(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename));
                }
                return database;
            }
        }
    }
}
