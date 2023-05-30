using System;
using System.Collections.Generic;
using StudentTernTracker.Models;
using StudentTernTracker.Services;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class AssesmentDetail : ContentPage
    {
        public AssesmentDetail(Assesment assesment)
        {
            InitializeComponent();

            AssessmentView.BindingContext = assesment;
        }

        async void EditAssement_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditAssesment());
        }

        async void DeleteAssesment_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Delete", $"Do you want to delete {Assesment.SelectedAssesmentName}", "Yes", "No");

            if (response == true)
            {
                await DatabaseService.RemoveAssessment(Assesment.SelectedAssessmentID);

                await Navigation.PopAsync();

            }
        }
    }
}
