using System;
using System.Collections.Generic;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class AddAssesment : ContentPage
    {
        public AddAssesment()
        {
            InitializeComponent();

            Type.ItemsSource = Assesment.typesList;
            Type.SelectedItem = Assesment.typesList[0].ToString();

        }

        async void SaveAssesment_Clicked(object sender, EventArgs e)
        {
            //check if values are entered
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                await DisplayAlert("Missing Assesment Name", "Please enter an assesment name", "OK");
                return;
            }
            if (EndDatePicker.Date < StartDatePicker.Date)
            {
                await DisplayAlert("Error", "The End Date can't be before the Start Date", "OK");
                return;
            }
            bool notification = Notification.IsToggled;
            await DatabaseService.AddAssesment(Name.Text, Type.SelectedItem.ToString(), Course.SelectedCourseId, StartDatePicker.Date, EndDatePicker.Date, notification);

            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }

}
