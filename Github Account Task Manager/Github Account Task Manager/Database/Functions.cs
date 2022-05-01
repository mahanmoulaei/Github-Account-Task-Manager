using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Functions
    {
        private static ObservableCollection<Models.Task> AllTasks = new ObservableCollection<Models.Task>();
        public static async Task<List<Models.User>> GetUsersAsync()
        {
            return await Config.GetDatabaseConnection().Table<Models.User>().ToListAsync();
        }

        public static async Task<ObservableCollection<Models.Task>> GetTasksAsync()
        {
            AllTasks.Clear();
            
            foreach (Models.Task task in await Config.GetDatabaseConnection().Table<Models.Task>().ToListAsync())
            {
                AllTasks.Add(task);
            }
            return AllTasks;
        }
    }
}
