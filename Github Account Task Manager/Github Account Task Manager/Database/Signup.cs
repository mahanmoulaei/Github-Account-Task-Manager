using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Signup
    {

        public static async Task<bool> Validate(string username, string password)
        {
            if (!String.IsNullOrEmpty(username))
            {
                if (!String.IsNullOrEmpty(password))
                {
                    try
                    {
                        await SaveUserAsync(username, password);
                        await App.Current.MainPage.DisplayAlert("Alert", "User " + username + " Created!", "OK");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //TODO
                        await App.Current.MainPage.DisplayAlert("Alert", ex.Message.ToString(), "Ok");
                        return false;
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Password field cannot be empty!", "Ok");
                    return false;
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Username field cannot be empty!", "Ok");
                return false;
            }
        }

        private static async Task<bool> SaveUserAsync(string username, string password)
        {
            foreach (Models.User user in await Functions.GetUsersAsync())
            {
                if (user.Username == username)
                {
                    return false;
                }
            }
            await Config.GetDatabaseConnection().InsertAsync(new Models.User() { Username = username, Password = password });
            return true;
        }
    }
}
