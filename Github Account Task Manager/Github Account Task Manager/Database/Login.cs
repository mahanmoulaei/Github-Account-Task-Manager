using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Github_Account_Task_Manager.Database
{
    internal class Login
    {
        static public async Task<bool> Validate(string email, string password)
        {
            if (!String.IsNullOrEmpty(email))
            {
                if (!String.IsNullOrEmpty(password))
                {
                    try
                    {
                        //TODO
                        await App.Current.MainPage.DisplayAlert("Alert", "Login for " + email + " was successful!", "OK");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        //TODO
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
    }
}
