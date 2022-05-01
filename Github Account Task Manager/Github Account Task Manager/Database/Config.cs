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
        public static readonly SQLiteAsyncConnection _database;
        
        static Config()
        {
            _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename));
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Task>().Wait();
        }

        public static SQLiteAsyncConnection GetDatabaseConnection()
        {
            return _database;
        }

        static Config database;
        internal static Config Database
        {
            get
            {
                if (database == null)
                {
                    //string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseFilename);
                    database = new Config();
                }
                return database;
            }
        }
    }
}
