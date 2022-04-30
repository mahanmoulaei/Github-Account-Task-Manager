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

        private void btnLogin_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnSignup_Clicked(object sender, EventArgs e)
        {
            ClearFields();
            await Navigation.PushAsync(new SignupPage());
        }

        private void ClearFields()
        {
            txtUsername.Text = txtPassword.Text = "";
        }
    }
}