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

            await RefreshTaskListAsync();
        }

        private async void listTasks_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.Task selectedTask = (Models.Task)e.SelectedItem;
            //var selectedTask = (await Database.Functions.GetTasksAsync()).Where(item => item.ID == temp.ID);
            var taskDetail = new TaskDetailPage(selectedTask);  //for date and assignee
            taskDetail.BindingContext = selectedTask;           //for bindings
            await Navigation.PushAsync(taskDetail);
        }

        private async void btnAddTask_Clicked(object sender, EventArgs e)
        {
            Models.User temp = (Models.User)pickerUser.SelectedItem;
            if (await Database.Task.Add(txtTaskID.Text, txtDescription.Text, temp.Username, datePickerDate.Date))
            {
                ResetFields();
                await RefreshTaskListAsync();
            }
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Alert", "Are you sure you want to logout?", "Yes", "No"))
            {
                await Navigation.PopAsync();
            }          
        }

        private async Task RefreshTaskListAsync()
        {
            listTasks.ItemsSource = null;
            listTasks.ItemsSource = await Database.Functions.GetTasksAsync();
        }

        private void ResetFields()
        {
            txtTaskID.Text = txtDescription.Text = "";
            pickerUser.SelectedIndex = 0;
            datePickerDate.Date = DateTime.Now;
        }
    }
}