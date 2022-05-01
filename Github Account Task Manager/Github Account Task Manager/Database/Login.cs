using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Login
    {
        private static bool passwordDoesntMatch = false;
        public static async Task<bool> Validate(string username, string password)
        {
            if (!String.IsNullOrEmpty(username))
            {
                if (!String.IsNullOrEmpty(password))
                {
                    try
                    {
                        if (await LoginUserAsync(username, password))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Login for " + username + " was successful!", "OK");
                            return true;
                        }
                        else
                        {
                            if (passwordDoesntMatch)
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Entered password for username(" + username + ") was wrong!", "OK");
                                passwordDoesntMatch = false;
                                return false;
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "No such username(" + username + ") was found in the database!", "OK");
                                return false;
                            }
                            
                        }                      
                    }
                    catch (Exception ex)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", ex.Message.ToString(), "OK");
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
                await App.Current.MainPage.DisplayAlert("Alert", "Username/Email field cannot be empty!", "Ok");
                return false;
            }
        }

        private static async Task<bool> LoginUserAsync(string username, string password)
        {
            foreach (Models.User user in await Functions.GetUsersAsync())
            {
                if (user.Username == username)
                {
                    if (user.Password == password)
                    {
                        return true;
                    }
                    else
                    {
                        passwordDoesntMatch = true;
                        return false;
                    }
                    
                }
            }
            return false;
        }
    } 
}
