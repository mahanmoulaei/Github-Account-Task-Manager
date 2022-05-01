using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Github_Account_Task_Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            MakeFieldsEnabled(false);
        }

        override
        protected void OnAppearing()
        {
            MakeFieldsEnabled(false);
        }

        private async void btnSearch_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                if (await CheckGithubUsername(txtUsername.Text))
                {
                    MakeFieldsEnabled(true);
                }
                else
                {
                    await DisplayAlert("Alert", "No GitHub account was found with \"" + txtUsername.Text + "\" username!", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Username field cannot be empty!", "Ok");
            }
        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                if (!String.IsNullOrEmpty(txtPassword.Text))
                {
                    if (!String.IsNullOrEmpty(txtPasswordRep.Text))
                    {
                        if (txtPassword.Text == txtPasswordRep.Text)
                        {
                            if (await Database.Signup.Validate(txtUsername.Text, txtPassword.Text))
                            {
                                GoBackToPreviousPageAsync();
                            }                        
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Password and password repetition do not match!", "Ok");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Password repetition field cannot be empty!", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Alert", "Password field cannot be empty!", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Alert", "Username field cannot be empty!", "Ok");
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            GoBackToPreviousPageAsync();
        }

        private async void GoBackToPreviousPageAsync()
        {
            MakeFieldsEnabled(false);
            await Navigation.PopAsync();
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != e.NewTextValue)
            {
                imgAvatar.Source = "profile.png";
                MakeFieldsEnabled(false);
            }
        }

        private async Task<bool> CheckGithubUsername(string username)
        {
            try
            {
                string url = "https://api.github.com/users/" + username;
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(url);
                var json = JsonConvert.DeserializeObject<Models.GitHubAccount>(response);
                imgAvatar.Source = json.Avatar;
                return true;             
            }
            catch
            {
                return false;
            }
            
        }

        private void MakeFieldsEnabled(bool state)
        {
            IsPasswordEntryEnabled(state);
            IsButtonCreateEnabled(state);
            IsButtonSearchEnabled(!state);
        }

        private void IsPasswordEntryEnabled(bool state)
        {
            txtPassword.Text = txtPasswordRep.Text = "";
            txtPassword.IsEnabled = state;
            txtPasswordRep.IsEnabled = state;
        }

        private void IsButtonCreateEnabled(bool state)
        {
            btnCreate.IsEnabled = state;
        }

        private void IsButtonSearchEnabled(bool state)
        {
            btnSearch.IsEnabled = state;
        }
    }
}