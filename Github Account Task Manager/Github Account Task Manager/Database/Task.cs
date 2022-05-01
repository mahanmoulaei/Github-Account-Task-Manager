using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Task
    {
        public static async Task<bool> Add(string id, string desciption, string assigned, DateTime deadline)
        {
            if (!String.IsNullOrEmpty(id))
            {
                if (!String.IsNullOrEmpty(desciption))
                {
                    if (!String.IsNullOrEmpty(assigned))
                    {
                        string deadlineString = deadline.ToShortDateString();
                        if (!String.IsNullOrEmpty(deadlineString))
                        {
                            try
                            {
                                if (await AddTaskAsync(id, desciption, assigned, deadlineString))
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Task with the ID of \"" + id + "\" added successfully!", "OK");
                                    return true;
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Task with the ID of \"" + id + "\" already exists!", "OK");
                                    return false;
                                }
                            }
                            catch (Exception ex)
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", ex.Message.ToString(), "Ok");
                                return false;
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Task Deadline field cannot be empty!", "Ok");
                            return false;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Task Assigned field cannot be empty!", "Ok");
                        return false;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Task Desription field cannot be empty!", "Ok");
                    return false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Task ID field cannot be empty!", "Ok");
                return false;
            }
        }

        private static async Task<bool> AddTaskAsync(string id, string desciption, string assigned, string deadline)
        {
            foreach (Models.Task task in await Functions.GetTasksAsync())
            {
                if (task.ID == id)
                {
                    return false;
                }
            }
            await Config.GetDatabaseConnection().InsertAsync(new Models.Task() { ID = id, Description = desciption, Assigned = assigned, Deadline = deadline });
            return true;
        }

        public static async Task<bool> Delete(Models.Task task)
        {
            try
            {
                if (await DeleteTaskAsync(task))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Task with the ID of \"" + task.ID + "\" deleted successfully!", "OK");
                    return true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Error happenned at deleting the task with the ID of \"" + task.ID + "\"!", "OK");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message.ToString(), "Ok");
                return false;
            }
        }

        private static async Task<bool> DeleteTaskAsync(Models.Task task)
        {
            if (await Config.GetDatabaseConnection().DeleteAsync(task) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }         
        }

        public static async Task<bool> Edit(Models.Task task)
        {
            try
            {
                if (await EditTaskAsync(task))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Task with the ID of \"" + task.ID + "\" edited successfully!", "OK");
                    return true;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Error happenned at editing the task with the ID of \"" + task.ID + "\"!", "OK");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message.ToString(), "Ok");
                return false;
            }
        }

        private static async Task<bool> EditTaskAsync(Models.Task task)
        {
            if (await Config.GetDatabaseConnection().UpdateAsync(task) >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
