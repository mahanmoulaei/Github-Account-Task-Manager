using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Github_Account_Task_Manager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailPage : ContentPage
    {
        Models.Task globakTask;
        public TaskDetailPage(Models.Task selectedTask)
        {
            InitializeComponent();
          
            globakTask = selectedTask;         
        }

        override
        protected async void OnAppearing()
        {
            datePickerDate.MinimumDate = DateTime.Now;
            datePickerDate.Date = Convert.ToDateTime(globakTask.Deadline);
            List<Models.User> usersList = await Database.Functions.GetUsersAsync();
            pickerUser.ItemsSource = usersList;
            pickerUser.SelectedIndex = usersList.FindIndex(item => item.Username == globakTask.Assigned);
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTaskID.Text))
            {
                if (!String.IsNullOrEmpty(txtDescription.Text))
                {
                    globakTask.ID = txtTaskID.Text;
                    globakTask.Description = txtDescription.Text;
                    globakTask.Assigned = ((Models.User)pickerUser.SelectedItem).Username;
                    globakTask.Deadline = datePickerDate.Date.ToShortDateString();
                    if (await Database.Task.Edit(globakTask))
                    {
                        GoBackToPreviousPage();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Task Description field cannot be empty!", "Ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Task ID field cannot be empty!", "Ok");
            }
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Alert", "Are you sure you want to Delete \"" + txtTaskID.Text + "\" task? It cannot be undone!", "Yes", "No"))
            {
                if (await Database.Task.Delete(globakTask))
                {
                    GoBackToPreviousPage();
                }
            }
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            GoBackToPreviousPage();
        }

        private async void GoBackToPreviousPage()
        {
            await Navigation.PopAsync();
        }
    }
}