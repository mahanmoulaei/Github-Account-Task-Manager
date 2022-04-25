using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Signup
    {

        static public async Task<bool> Validate(string username, string password)
        {
            if (!String.IsNullOrEmpty(username))
            {
                if (!String.IsNullOrEmpty(password))
                {
                    try
                    {
                        await SaveUserAsync(
                            new Models.User
                            {
                                Username = username,
                                Password = password
                            }
                        );
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

        private static Task<int> SaveUserAsync(Models.User user)
        {
            return Config.GetDatabaseConnection().InsertAsync(user);
        }
    }
}
