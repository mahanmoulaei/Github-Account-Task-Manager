using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Functions
    {
        public static async Task<List<Models.User>> GetUsersAsync()
        {
            return await Config.GetDatabaseConnection().Table<Models.User>().ToListAsync();
        }

        public static async Task<List<Models.Task>> GetTasksAsync()
        {
            return await Config.GetDatabaseConnection().Table<Models.Task>().ToListAsync();
        }
    }
}
