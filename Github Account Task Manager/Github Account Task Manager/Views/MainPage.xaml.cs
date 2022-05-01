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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        override
        protected async void OnAppearing()
        {
            datePickerDate.MinimumDate = DateTime.Now;

            pickerUser.ItemsSource = await Database.Functions.GetUsersAsync();
            pickerUser.SelectedIndex = 0;

            listTasks.ItemsSource = await Database.Functions.GetTasksAsync();
        }

        private void listTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void btnAddTask_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Alert", "Are you sure you want to logout?", "Yes", "No"))
            {
                await Navigation.PopAsync();
            }          
        }
    }
}