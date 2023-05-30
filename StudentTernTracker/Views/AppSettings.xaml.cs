using System;
using System.Collections.Generic;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class AppSettings : ContentPage
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        async void SampleDataBtn_Clicked(object sender, EventArgs e)
        {
            if(Settings.DummyDataLoaded == false)
            {
                Settings.DummyDataLoaded = true;
                await DatabaseService.AddDummyData();
            } else
            {
                await DisplayAlert("Error", "Dummy data already loaded", "Ok");
            }
        }
    }
}
