using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Functions
    {
        public Task<List<Models.User>> GetUsersAsync()
        {
            return Config.GetDatabaseConnection().Table<Models.User>().ToListAsync();
        }
    }
}
