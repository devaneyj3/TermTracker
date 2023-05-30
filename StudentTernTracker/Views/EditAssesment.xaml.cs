using System;
using System.Collections.Generic;
using StudentTernTracker.Services;
using StudentTernTracker.Models;
using Xamarin.Forms;

namespace StudentTernTracker.Views
{
    public partial class EditAssesment : ContentPage
    {
        public EditAssesment()
        {
            InitializeComponent();

            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //populate course details
            var assessments = await DatabaseService.GetAssesment(Assesment.SelectedAssessmentID);

            TypePicker.ItemsSource = Assesment.typesList;


            foreach (Assesment assessment in assessments)
            {

                //prepopulate form
                AssesmentID.Text = assessment.Id.ToString();
                AssesmentName.Text = assessment.Name;
                TypePicker.SelectedItem = assessment.Type;
                StartDatePicker.Date = assessment.Start;
                EndDatePicker.Date = assessment.End;
            }
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void EditAssesment_Clicked(object sender, EventArgs e)
        {
            //Get Values from fields
            string assessmentName = AssesmentName.Text;
            string assesmentType = TypePicker.SelectedItem.ToString();
            DateTime assessmentStartDate = StartDatePicker.Date;
            DateTime assessmentEndDate = EndDatePicker.Date;

            //check if values are entered
            if (string.IsNullOrWhiteSpace(assessmentName))
            {
                await DisplayAlert("Missing Assesment Name", "Please enter an assesment name", "OK");
                return;
            }

            if (assessmentEndDate < assessmentStartDate)
            {
                await DisplayAlert("Error", "The End Date can't be before the Start Date", "OK");
                return;
            }
            await DatabaseService.UpdateAssessment(Assesment.SelectedAssessmentID, assessmentName, assesmentType, assessmentStartDate, assessmentEndDate);

            await DisplayAlert("Updated", "Assessment is updated", "OK");
            await Navigation.PopToRootAsync();
        }
    }
}
