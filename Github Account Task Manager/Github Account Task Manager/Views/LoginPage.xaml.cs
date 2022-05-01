using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Github_Account_Task_Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtUsername.Text))
            {
                if (!String.IsNullOrEmpty(txtPassword.Text))
                {
                    if (await Database.Login.Validate(txtUsername.Text, txtPassword.Text))
                    {
                        LoadPage(new MainPage());
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

        private void btnSignup_Clicked(object sender, EventArgs e)
        {
            LoadPage(new SignupPage());
        }

        private void ClearFields()
        {
            txtUsername.Text = txtPassword.Text = "";
        }

        private async void LoadPage(Page page)
        {
            ClearFields();
            await Navigation.PushAsync(page);
        }
    }
}