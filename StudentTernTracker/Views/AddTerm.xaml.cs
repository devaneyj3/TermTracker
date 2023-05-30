using System;
using System.Collections.Generic;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class AddTerm : ContentPage
    {
        public AddTerm()
        {
            InitializeComponent();
        }

        async void SaveTerm_Clicked(object sender, EventArgs e)
        {
            string Name = TermName.Text;
            DateTime Start = StartDatePicker.Date;
            DateTime End = EndDatePicker.Date;

            //check if values are entered
            if (string.IsNullOrWhiteSpace(Name))
            {
                await DisplayAlert("Missing Term Name", "Please enter a term name", "OK");
                return;
            }

            if (Start < End)
            {
                await DisplayAlert("Error", "The End Date can't be before the Start Date", "OK");
                return;
            }

            await DisplayAlert("Title", "Saving Term", "OK");
            await DatabaseService.AddTerm(Name, Start, End);
            await Navigation.PopAsync();


        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
